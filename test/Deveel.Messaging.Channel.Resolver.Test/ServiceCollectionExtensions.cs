using Deveel.Messaging.Channels;

using Microsoft.Extensions.DependencyInjection;

namespace Deveel.Messaging {
	public static class ServiceCollectionExtensions {
		public static IServiceCollection AddTestChannelResolver(this IServiceCollection services, IEnumerable<IChannel>? channels = null) {
			services.AddSingleton<TestChannelResolver>(sp => new TestChannelResolver(channels));
			services.AddSingleton<IChannelResolver>(sp => sp.GetRequiredService<TestChannelResolver>());
			return services;
		}
	}
}
