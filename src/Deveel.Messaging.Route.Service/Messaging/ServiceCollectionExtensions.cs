using Deveel.Messaging.Routes;

using Microsoft.Extensions.DependencyInjection;

namespace Deveel.Messaging {
	/// <summary>
	/// Provides extensions to the <see cref="IServiceCollection"/>
	/// to register the services of the route selector.
	/// </summary>
	public static class ServiceCollectionExtensions {
		/// <summary>
		/// Registers the services of the route selector.
		/// </summary>
		/// <param name="services">
		/// A collection of services to register the route selector.
		/// </param>
		/// <param name="lifetime">
		/// The lifetime of the services to register.
		/// </param>
		/// <returns>
		/// Returns the given collection of services.
		/// </returns>
		public static IServiceCollection AddRouteSelector(this IServiceCollection services, ServiceLifetime lifetime = ServiceLifetime.Scoped) {
			services.Add(new ServiceDescriptor(typeof(IRouteSelector), typeof(RouteSelector), lifetime));
			services.Add(new ServiceDescriptor(typeof(RouteSelector), typeof(RouteSelector), lifetime));
			return services;
		}
	}
}
