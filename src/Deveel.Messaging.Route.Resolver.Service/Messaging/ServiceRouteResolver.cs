using Deveel.Messaging.Routes;

namespace Deveel.Messaging {
	public sealed class ServiceRouteResolver : IMessageRouteResolver {
		private readonly IRouteSelector routeSelector;

		public ServiceRouteResolver(IRouteSelector routeSelector) {
			this.routeSelector = routeSelector;
		}

		public Task<IMessageRoute?> FindByIdAsync(string id, RouteResolutionOptions? options = null, CancellationToken cancellationToken = default) {
			return routeSelector.SelectRouteByIdAsync(options?.TenantId, id, cancellationToken);
		}

		public async Task<IRouteChannel?> ResolveChannelAsync(IMessage message, RouteResolutionOptions? options = null, CancellationToken cancellationToken = default) {
			var route = await routeSelector.SelectRouteAsync(message, cancellationToken);
			return route?.Channel;
		}
	}
}
