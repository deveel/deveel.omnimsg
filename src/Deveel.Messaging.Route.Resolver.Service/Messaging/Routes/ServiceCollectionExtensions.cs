using Microsoft.Extensions.DependencyInjection;

namespace Deveel.Messaging.Routes {
	public static class ServiceCollectionExtensions {
		public static IServiceCollection AddServiceRouteResolver(this IServiceCollection services) {
			services.AddTransient<IMessageRouteResolver, ServiceRouteResolver>();
			services.AddTransient<ServiceRouteResolver>();

			return services;
		}
	}
}
