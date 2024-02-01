// Copyright 2023 Deveel AS
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using Deveel.Messaging.Channels;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;

using Polly;
using Polly.Timeout;

namespace Deveel.Messaging {
	/// <summary>
	/// A service that provides the ability to send messages 
	/// to a channel.
	/// </summary>
	/// <remarks>
	/// <para>
	/// A sender service is indicated for scenarios of mass 
	/// sending of messages from a single source to a channel:
	/// the processes of resolution of channels and connectors
	/// might be expensive and time consuming, so it is better
	/// to have a single service that is able to send messages
	/// when needed.
	/// </para>
	/// <para>
	/// When dealing with a chat context, where messages are
	/// exchanged between two or more parties, with a near-realtime
	/// scenario, it is better to user other type of services, which
	/// also involve a receiver.
	/// </para>
	/// </remarks>
	public class MessageSender {
		private readonly MessageSenderOptions options;
		private readonly IChannelConnectorResolver connectorResolver;
		private readonly IMessageLogger messageLogger;
		private readonly IChannelResolver channelResolver;
		private readonly IMessageStateHandler? stateHandler;
		private readonly ILogger logger;

		/// <summary>
		/// Creates an instance of the sender service.
		/// </summary>
		/// <param name="options">
		/// A set of options that configure the behavior of the sender.
		/// </param>
		/// <param name="channelResolver">
		/// A service that resolves the channels used to configure
		/// the connections to the providers.
		/// </param>
		/// <param name="connectorResolver">
		/// A service that resolves the connectors to the providers
		/// of messaging services.
		/// </param>
		/// <param name="stateHandler">
		/// An optional handler that is invoked when a local message 
		/// state is available.
		/// </param>
		/// <param name="messageLogger">
		/// A logger of the messages that are handled by the sender.
		/// </param>
		/// <param name="logger">
		/// A logger to record the activity of the sender.
		/// </param>
		public MessageSender(
			IOptions<MessageSenderOptions> options,
			IChannelResolver channelResolver,
			IChannelConnectorResolver connectorResolver,
			IMessageStateHandler? stateHandler = null,
			IMessageLogger? messageLogger = null,
			ILogger<MessageSender>? logger = null) {
			this.options = options.Value;
			this.stateHandler = stateHandler;
			this.channelResolver = channelResolver;
			this.connectorResolver = connectorResolver;
			this.messageLogger = messageLogger ?? NullMessageLogger.Instance;
			this.logger = logger ?? NullLogger<MessageSender>.Instance;
		}

		/// <summary>
		/// A callback invoked when a message state is handled.
		/// </summary>
		/// <param name="messageState">
		/// The state of the message that was handled.
		/// </param>
		/// <param name="cancellationToken">
		/// A cancellation token that can be used to cancel the operation.
		/// </param>
		/// <returns>
		/// </returns>
		protected virtual Task OnMessageStateAsync(IMessageState messageState, CancellationToken cancellationToken = default) {
			return Task.CompletedTask;
		}

		private async Task HandleStateAsync(IMessageState state, CancellationToken cancellationToken = default) {
			using var scope = logger.BeginScope("TenantId: {TenantId}", state.TenantId);

			try {
				await messageLogger.LogMessageStateAsync(state, cancellationToken);
			} catch (Exception ex) {
				throw new MessagingException(MessagingErrorCode.UnknownError, "An error occurred while logging the message state", ex);
			}

			if (options.StateHandler != null) {
				await options.StateHandler(state);
			}

			if (stateHandler != null) {
				try {
					await stateHandler.HandleAsync(state, cancellationToken);
				} catch (Exception ex) {
					logger.WarnCallbackError(ex, state.MessageId, state.Status);
				}
			}

			try {
				await OnMessageStateAsync(state, cancellationToken);
			} catch (Exception ex) {
				// TODO: log this error as a warning ...
			}
		}

		private Task OnMessageSentAsync(IMessage message, int attemptCount, CancellationToken cancellationToken) {
			return HandleStateAsync(LocalMessageState.Sent(message.WithAttempt(attemptCount)), cancellationToken);
		}

		private Task OnFailedAsync(IMessage message, int attempt, IMessageError error, CancellationToken cancellationToken) {
			return HandleStateAsync(LocalMessageState.DeliveryFailed(message.WithAttempt(attempt), error), cancellationToken);
		}

		/// <summary>
		/// Resolves a channel connector for the given channel type and provider.
		/// </summary>
		/// <param name="channelType">
		/// The type of the channel to resolve.
		/// </param>
		/// <param name="channelProvider">
		/// The identifier of the provider of the channel.
		/// </param>
		/// <param name="cancellationToken">
		/// A cancellation token that can be used to cancel the operation.
		/// </param>
		/// <returns>
		/// Returns an instance of <see cref="IChannelConnector"/> that can be
		/// used to send or messages through the channel.
		/// </returns>
		protected virtual async Task<IChannelConnector?> ResolveChannelConnectorAsync(string channelType, string channelProvider, CancellationToken cancellationToken = default) {
			// TODO: here we should implement some sort of caching,
			//       so that we don't resolve the same connector multiple times
			//       for the same channel type and provider

			await Task.Yield();

			return connectorResolver.Resolve(channelType, channelProvider);
		}

		/// <summary>
		/// Sends a message to the destination, through the
		/// channel referenced by the message itself.
		/// </summary>
		/// <param name="message">
		/// The instance of the message to send.
		/// </param>
		/// <param name="cancellationToken">
		/// A cancellation token that can be used to cancel the operation.
		/// </param>
		/// <returns>
		/// Returns a <see cref="MessageResult"/> that describes the result
		/// of the sending operation.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// Thrown when the given <paramref name="message"/> is <c>null</c>.
		/// </exception>
		/// <exception cref="ArgumentException">
		/// Thrown when the given <paramref name="message"/> has no channel
		/// specified.
		/// </exception>
		public virtual async Task<MessageResult> SendAsync(IMessage message, CancellationToken cancellationToken = default) {
			ArgumentNullException.ThrowIfNull(message, nameof(message));

			if (message.Channel == null)
				throw new ArgumentException("The message has no channel specified", nameof(message));

			using var loggerScope = logger.BeginScope("TenantId: {TenantId}", message.TenantId);

			try {
				logger.TraceResolvingConnector(message.Channel.Type, message.Channel.Provider);

				var connector = await ResolveChannelConnectorAsync(message.Channel.Type, message.Channel.Provider, cancellationToken);

				if (connector == null) {
					logger.LogChannelNotAvailable(message.Channel.Type, message.Channel.Provider);
					return MessageResult.Fail(MessagingErrorCode.ChannelNotSupported, $"No connector found for {message.Channel.Type} channel by {message.Channel.Provider}");
				}

				var channel = await FindChannelAsync(message.TenantId, message.Channel, cancellationToken);

				if (channel == null) {
					logger.LogChannelNotFound(message.Channel.Type, message.Channel.Provider, message.Channel.Name);
					return MessageResult.Fail(MessagingErrorCode.ChannelNotFound, "No channel found for the message");
				}

				if (!channel.IsActive()) {
					logger.LogChannelNotActive(message.Channel.Type, message.Channel.Provider, message.Channel.Name);
					return MessageResult.Fail(MessagingErrorCode.ChannelNotActive, $"The channel {channel.Name} is not active");
				}

				await LogMessageAsync(message, cancellationToken);

				var retryCount = message.RetryCount(channel, options.Retry?.MaxRetries);

				using var connection = connector.Connect(channel);
				var sender = connection.CreateSender();
				var result = await SendMessageAsync(sender, message, channel, retryCount, cancellationToken);

				if (result.Successful) {
					logger.TraceMessageSent(message.Id, channel.Type, channel.Provider, channel.Name);
				} else {
					logger.WarnMessageNotSent(message.Id, channel.Type, channel.Provider, channel.Name, result.Error!.Code, result.Error!.Message);
				}

				return result;
			} catch (MessagingException ex) {
				logger.LogUnknownSenderError(ex);
				return MessageResult.Fail(ex);
			} catch (Exception ex) {
				logger.LogUnknownSenderError(ex);
				return MessageResult.Fail(MessagingErrorCode.UnknownError, "Unable to send the message for an unknown error");
			}
		}

		/// <summary>
		/// Sends the given message through the channel sender.
		/// </summary>
		/// <param name="sender">
		/// The channel sender to use to send the message.
		/// </param>
		/// <param name="message">
		/// The message that is to be sent.
		/// </param>
		/// <param name="cancellationToken">
		/// A cancellation token that can be used to cancel the operation.
		/// </param>
		/// <returns>
		/// Returns a <see cref="MessageResult"/> that describes the result
		/// of the sending operation.
		/// </returns>
		protected virtual async Task<MessageResult> SendMessageAsync(IChannelSender sender, IMessage message, CancellationToken cancellationToken = default) {
			return await sender.SendAsync(message, cancellationToken);
		}

		private async Task<MessageResult> SendMessageWithTimeoutAsync(IChannelSender sender, IMessage message, IChannel channel, CancellationToken cancellationToken) {
			var timeout = message.Timeout(channel, options.SendTimeout);

			var policy = Policy
				.TimeoutAsync(timeout, Polly.Timeout.TimeoutStrategy.Pessimistic);

			var capture = await policy.ExecuteAndCaptureAsync<MessageResult>((token) => SendMessageAsync(sender, message, token), cancellationToken);

			if (capture.Outcome == OutcomeType.Successful)
				return capture.Result;

			if (capture.FaultType == FaultType.ExceptionHandledByThisPolicy) {
				return MessageResult.Fail(MessagingErrorCode.ChannelTimeout, "The channel timed out");
			}

			if (capture.FaultType == FaultType.UnhandledException &&
				capture.FinalException is TimeoutRejectedException ex)
				return MessageResult.Fail(MessagingErrorCode.ChannelTimeout, "The channel timed out");

			return MessageResult.Fail(MessagingErrorCode.ChannelUnknownError, "An unknown error occurred while sending the message");
		}

		private async Task<MessageResult> SendMessageAsync(IChannelSender sender, IMessage message, IChannel channel, int retryCount, CancellationToken cancellationToken) {
			var contextData = new Dictionary<string, object> {
				{ "message", message.WithAttempt(1) }
			};

			var capture = await Policy
				.Handle<ChannelTransientException>()
				.OrResult<MessageResult>((result) => {
					return !result.Successful && 
					(result.Error != null && result.Error is IChannelMessageError channelError && channelError.ErrorType == MessageErrorType.Transient);
				})
				.WaitAndRetryAsync(retryCount, retryAttempt => SleepDuration(retryAttempt, message.RetryDelay()), (ex, ts, attempt, context) => {
					var msg = (IMessage)context["message"];
					msg = msg.WithAttempt(attempt + 1);

					using var loggerScope = logger.BeginScope("TenantId: {TenantId}", msg.TenantId);
					logger.TraceMessageRetrySend(msg.Id, channel.Type, channel.Provider, channel.Name);

					context["message"] = msg;
				})
				.ExecuteAndCaptureAsync(async (Context context, CancellationToken token) => {
					var msg = (IMessage)context["message"];
					return await SendMessageWithTimeoutAsync(sender, msg, channel, token);
				}, contextData, cancellationToken);

			if (capture.Outcome == OutcomeType.Successful) {
				await OnMessageSentAsync(message, message.Attempt() ?? 1, cancellationToken);
				return capture.Result;
			} else {
				string? errorCode = null;
				string? errorMessage = null;
				IMessageError? innerError = null;

				if (capture.FaultType == FaultType.ResultHandledByThisPolicy) {
					errorCode = MessagingErrorCode.ChannelNotAvailable;
					errorMessage = "Unable to route the message";
					innerError = capture.FinalHandledResult.Error;
				} else if (capture.FaultType == FaultType.ExceptionHandledByThisPolicy &&
					capture.FinalException is ChannelTransientException ex) {
					if (ex is ChannelTimeoutException toex) {
						errorCode = MessagingErrorCode.ChannelTimeout;
						errorMessage = "The channel timed out";
					} else {
						errorCode = MessagingErrorCode.ChannelNotAvailable;
						errorMessage = "Unable to route the message";
						innerError = ex;
					}
				} else if (capture.FinalException is ChannelTerminalException tex) {
					errorCode = tex.ErrorCode;
					errorMessage = tex.Message;
				} else {
					errorCode = MessagingErrorCode.ChannelUnknownError;
					errorMessage = "An unknown error occurred while sending the message";
				}

				var msg = (IMessage)capture.Context["message"];
				await OnFailedAsync(msg, msg.Attempt() ?? 1, ChannelMessageError.Transient(errorCode, errorMessage, innerError), cancellationToken);

				return MessageResult.Fail(errorCode, errorMessage, innerError);
			}
		}

		private TimeSpan SleepDuration(int retryAttempt, int? msgDelayMillis) {
			var retryType = options.Retry?.Strategy ?? MessageRetryStrategy.ExponentialBackoff;
			var retryDelay = msgDelayMillis != null
				? TimeSpan.FromMilliseconds(msgDelayMillis.Value)
				: options.Retry?.Delay ?? TimeSpan.FromMilliseconds(300);

			return retryType switch {
				MessageRetryStrategy.ExponentialBackoff => TimeSpan.FromMilliseconds(Math.Pow(2, retryAttempt) * retryDelay.TotalMilliseconds),
				MessageRetryStrategy.LinearBackoff => TimeSpan.FromMilliseconds(retryAttempt * retryDelay.TotalMilliseconds),
				_ => retryDelay,
			};
		}

		/// <summary>
		/// Logs the given message that is to be sent.
		/// </summary>
		/// <param name="message">
		/// The message to log.
		/// </param>
		/// <param name="cancellationToken">
		/// A cancellation token that can be used to cancel the operation.
		/// </param>
		/// <returns>
		/// Returns a task that when completed logs the message.
		/// </returns>
		protected virtual Task LogMessageAsync(IMessage message, CancellationToken cancellationToken) {
			if (!options.LogMessages)
				return Task.CompletedTask;

			return messageLogger.LogMessageAsync(message, cancellationToken);
		}

		private async Task<IChannel?> FindChannelAsync(string? tenantId, IMessageChannel channel, CancellationToken cancellationToken) {
			ArgumentNullException.ThrowIfNull(channel, nameof(channel));

			// TODO: here we should implement some sort of caching,
			//       so that we don't resolve the same channel multiple times
			//       for the same channel type, provider and tenant

			var options = new ChannelResolutionOptions {
				TenantId = tenantId,
				IncludeCredentials = true
			};

			IChannel? result = null;

			using var scope = this.logger.BeginScope("TenantId: {TenantId}", tenantId);

			if (!String.IsNullOrWhiteSpace(channel.Id)) {
				logger.TraceResolvingChannelById(channel.Id);

				result = await channelResolver.FindByIdAsync(channel.Id, options, cancellationToken);
			} else if (!String.IsNullOrEmpty(channel.Name)) {
				logger.TraceResolvingChannelByName(channel.Name);

				result = await channelResolver.FindByNameAsync(channel.Name, options, cancellationToken);
			} else {
				throw new ChannelTerminalException(MessagingErrorCode.ChannelNotFound, "No channel identifier specified");
			}

			if (result != null) {
				logger.TraceChannelResolved(result.Type, result.Provider, result.Name);
			}

			return result;
		}
	}
}
