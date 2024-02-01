using Deveel.Messaging.Channels;
using Deveel.Messaging.Routes;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;

namespace Deveel.Messaging {
	public class MessageReceiver {
		private readonly MessageReceiverOptions options;
		private readonly IChannelResolver channelResolver;
		private readonly IMessageRouteResolver? routeResolver;
		private readonly IMessageLogger messageLogger;
		private readonly ILogger logger;

		public MessageReceiver(
			IOptions<MessageReceiverOptions> options, 
			IChannelResolver channelResolver, 
			IMessageRouteResolver? routeResolver = null,
			IMessageLogger? messageLogger = null, 
			ILogger<MessageReceiver>? logger = null) {
			this.options = options.Value;
			this.channelResolver = channelResolver;
			this.routeResolver = routeResolver;
			this.messageLogger = messageLogger ?? NullMessageLogger.Instance;
			this.logger = logger ?? NullLogger<MessageReceiver>.Instance;
		}

		private async Task LogMessageAsync(IMessage message, CancellationToken cancellationToken = default) {
			if (options.LogMessages)
				await messageLogger.LogMessageAsync(message, cancellationToken);
		}

		private async Task<IMessage?> ReceiveMessageAsync(IMessageSource source, CancellationToken cancellationToken = default) {
			var message = await source.ReadAsMessageAsync(cancellationToken);

			if (message != null)
				await LogMessageAsync(message, cancellationToken);

			return message;
		}

		public async Task<MessageReceiveResult> ReceiveAsync(string? tenantId, IMessageSource source, CancellationToken cancellationToken = default) {
			var message = await ReceiveMessageAsync(source, cancellationToken);

			if (message == null)
				throw new MessagingException(MessagingErrorCodes.MessageNotRead, "The message could not be read from the source.");

			if (routeResolver == null)
				return MessageReceiveResult.Failed(MessagingErrorCodes.ChannelNotSupported, "The message receiver does not support routing.");

			var options = new RouteResolutionOptions { TenantId = tenantId };
			var channel = await routeResolver.ResolveChannelAsync(message, options, cancellationToken);

			if (channel == null)
				return MessageReceiveResult.Failed(MessagingErrorCodes.ChannelNotFound, "The message could not be routed to a channel.");

			message = message.WithChannel(new RouteMessageChannelWrapper(channel));

			return MessageReceiveResult.Success(message);
		}

		public async Task<MessageReceiveResult> ReceiveAsync(string? tenantId, string channelId, IMessageSource source, CancellationToken cancellationToken = default) {
			var options = new ChannelResolutionOptions { TenantId = tenantId, IncludeCredentials = false };
			var channel = await channelResolver.FindByIdAsync(channelId, options, cancellationToken);

			if (channel == null)
				return MessageReceiveResult.Failed(MessagingErrorCodes.ChannelNotFound, $"The channel '{channelId}' was not found.");

			if (channel.Type != source.Type ||
				channel.Provider != source.Provider)
				return MessageReceiveResult.Failed(MessagingErrorCodes.ChannelTypeMismatch, $"The channel '{channelId}' is not compatible with the source.");

			var message = await ReceiveMessageAsync(source, cancellationToken);

			if (message == null)
				throw new MessagingException(MessagingErrorCodes.MessageNotRead, "The message could not be read from the source.");

			message = message.WithChannel(new MessageChannelWrapper(channel));

			return MessageReceiveResult.Success(message);
		}

		class MessageChannelWrapper : IMessageChannel {
			private readonly IChannel channel;

			public MessageChannelWrapper(IChannel channel) {
				this.channel = channel;
			}

			public string Id => channel.Id;

			public string Type => channel.Type;

			public string Provider => channel.Provider;

			public string Name => channel.Name;
		}

		class RouteMessageChannelWrapper : IMessageChannel {
			private readonly IRouteChannel channel;

			public RouteMessageChannelWrapper(IRouteChannel channel) {
				this.channel = channel;
			}

			public string Id => channel.Id;

			public string Type => channel.Type;

			public string Provider => channel.Provider;

			public string Name => channel.Name;
		}
	}
}
