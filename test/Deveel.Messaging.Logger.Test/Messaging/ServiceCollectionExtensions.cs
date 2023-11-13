using Microsoft.Extensions.DependencyInjection;

namespace Deveel.Messaging {
	public static class ServiceCollectionExtensions {
		public static IServiceCollection AddTestMessageLogger(this IServiceCollection services, 
				Func<IMessage, CancellationToken, Task>? messageLogger = null, 
				Func<IMessageState, CancellationToken, Task>? stateLogger = null) {
			services.AddSingleton<IMessageLogger>(new TestMessageLogger(messageLogger, stateLogger));
			return services;
		}

		public static IServiceCollection AddTestMessageLogger(this IServiceCollection services, 
				Func<IMessage, Task>? messageLogger = null, 
				Func<IMessageState, Task>? stateLogger = null) {
			services.AddSingleton<IMessageLogger>(new TestMessageLogger(
				messageLogger == null ? null : (Func<IMessage, CancellationToken, Task>)((m, c) => messageLogger(m)),
				stateLogger == null ? null : (Func<IMessageState, CancellationToken, Task>)((s, c) => stateLogger(s))));

			return services;
		}

		public static IServiceCollection AddTestMessageLogger(this IServiceCollection services, 
				Action<IMessage>? messageLogger = null, 
				Action<IMessageState>? stateLogger = null) {
			services.AddSingleton<IMessageLogger>(new TestMessageLogger(
					messageLogger == null ? null : (Func<IMessage, CancellationToken, Task>)((m, c) => {
					messageLogger(m);
					return Task.CompletedTask;
				}),
					stateLogger == null ? null : (Func<IMessageState, CancellationToken, Task>)((s, c) => {
					stateLogger(s);
					return Task.CompletedTask;
				})));

			return services;
		}
	}
}
