using Deveel.Messaging;
using Deveel.Messaging.Channels;

using Microsoft.Extensions.DependencyInjection;

namespace Deveel {
	public static class ServiceCollectionExtensions {
		public static IServiceCollection AddTestConnector(this IServiceCollection services, IMessageSendCallback? sendCallback = null) {
			services.AddChannelConnector<TestConnector>(ServiceLifetime.Singleton);

			if (sendCallback != null)
				services.AddSingleton<IMessageSendCallback>(sendCallback);

			return services;
		}

		public static IServiceCollection AddTestConnector(this IServiceCollection services, Func<IMessage, CancellationToken, Task<MessageResult>>? onSend = null)
			=> services.AddTestConnector(new DelegatedMessageSendCallback(onSend));
	}
}
