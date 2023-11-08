using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Deveel.Messaging {
	public sealed class MessageSenderBuilder {
		internal MessageSenderBuilder(IServiceCollection services) {
			Services = services ?? throw new ArgumentNullException(nameof(services));

			AddDefaults();
		}

		public IServiceCollection Services { get; }

		private void AddDefaults() {
			Services.AddScoped<MessageSender>();
			// TODO: Services.AddScoped<MessageStateHandler>();
		}

		public MessageSenderBuilder UseSender(Type senderType, ServiceLifetime lifetime = ServiceLifetime.Scoped) {
			if (senderType == null)
				throw new ArgumentNullException(nameof(senderType));
			if (!typeof(MessageSender).IsAssignableFrom(senderType))
				throw new ArgumentException($"The type {senderType} is not a valid sender type.");

			Services.Add(new ServiceDescriptor(senderType, senderType, lifetime));

			if (senderType != typeof(MessageSender))
				Services.Add(new ServiceDescriptor(typeof(MessageSender), senderType, lifetime));

			return this;
		}

		public MessageSenderBuilder UseSender<TSender>(ServiceLifetime lifetime = ServiceLifetime.Scoped)
			where TSender : MessageSender
			=> UseSender(typeof(TSender), lifetime);

		public MessageSenderBuilder Configure(Action<MessageSenderOptions> configure) {
			if (configure == null)
				throw new ArgumentNullException(nameof(configure));

			Services.AddOptions<MessageSenderOptions>()
				.Configure(configure);

			return this;
		}

		public MessageSenderBuilder Configure(string sectionName) {
			if (string.IsNullOrWhiteSpace(sectionName))
				throw new ArgumentException($"'{nameof(sectionName)}' cannot be null or whitespace.", nameof(sectionName));

			Services.AddOptions<MessageSenderOptions>()
				.BindConfiguration(sectionName);

			return this;
		}

		//public MessageSenderBuilder HandleState(Func<IMessageState, Task> router) {
		//	Services.AddSingleton<IMessageStateRouter>(new DelegatedMessageStateRouter(router));

		//	return this;
		//}

		//public MessageSenderBuilder HandleState(Action<IMessageState> router) {
		//	Services.AddSingleton<IMessageStateRouter>(new DelegatedMessageStateRouter(router));

		//	return this;
		//}
	}
}
