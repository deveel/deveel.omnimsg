using Deveel.Messaging.Terminals;

using Microsoft.Extensions.DependencyInjection;

namespace Deveel.Messaging {
	public static class ServiceCollectionExtensions {
		public static IServiceCollection AddTestTerminalResolver(this IServiceCollection services, IEnumerable<IServiceTerminal>? terminals = null) {
			services.AddSingleton<ITerminalResolver>(sp => sp.GetRequiredService<TestTerminalResolver>());
			services.AddSingleton<TestTerminalResolver>(sp => new TestTerminalResolver(terminals));

			return services;
		}
	}
}
