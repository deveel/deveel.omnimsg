using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Deveel.Messaging.Routes {
	/// <summary>
	/// An implementation of <see cref="IRouteSelector"/> that
	/// is backed by a <see cref="IRouteRetriever"/> and a
	/// <see cref="IRouteFilterEvaluator"/>. to select the
	/// route to use for a message.
	/// </summary>
	public class RouteSelector : IRouteSelector {
		private readonly IRouteRetriever routeRetriever;
		// TODO: support multiple formats...
		private readonly IRouteFilterEvaluator? filterEvaluator;
		private readonly ILogger logger;

		// a cache of the routes per tenant, living for the lifetime of the selector
		private IDictionary<RouteCacheKey, IList<IMessageRoute>> cache = new Dictionary<RouteCacheKey, IList<IMessageRoute>>();

		/// <summary>
		/// Constructs the selector with the given route retriever and evaluator.
		/// </summary>
		/// <param name="routeRetriever">
		/// The service to use to retrieve the routes.
		/// </param>
		/// <param name="filterEvaluator">
		/// The service to use to evaluate the filters of the routes.
		/// </param>
		/// <param name="logger">
		/// A logger to use to log the operations of the selector.
		/// </param>
		public RouteSelector(IRouteRetriever routeRetriever, 
			IRouteFilterEvaluator? filterEvaluator = null, 
			ILogger<RouteSelector>? logger = null) {
			this.routeRetriever = routeRetriever;
			this.filterEvaluator = filterEvaluator;
			this.logger = logger ?? NullLogger<RouteSelector>.Instance;
		}

		/// <inheritdoc/>
		public async Task<IMessageRoute?> SelectRouteAsync(IMessage message, CancellationToken cancellationToken = default) {
			var routes = await RetrieveRoutes(message.TenantId, null, cancellationToken);

			if (routes == null || routes.Count == 0)
				return null;

			var direction = message.Direction == MessageDirection.Inbound ? RoutingDirection.Inbound : RoutingDirection.Outbound;
			var availableRoutes = routes.Where(x => x.Direction == direction).ToList();

			if (availableRoutes.Count == 0)
				return null;

			if (availableRoutes.Count == 1)
				return availableRoutes[0];

			var orderedList = routes.OrderBy(x => x.Priority);

			foreach (var route in orderedList) {
				if (await EvaluateAsync(route, message, cancellationToken))
					return route;
			}

			return null;
		}

		/// <inheritdoc/>
		public async Task<IMessageRoute?> SelectRouteByIdAsync(string? tenantId, string routeId, CancellationToken cancellationToken = default) {
			var routes = await RetrieveRoutes(tenantId, routeId, cancellationToken);

			if (routes == null || routes.Count == 0)
				return null;

			if (routes.Count == 1)
				return routes[0];

			throw new MessagingException("route.ambigous_id", $"The route id '{routeId}' is ambigous");
		}

		private async Task<bool> EvaluateAsync(IMessageRoute route, IMessage message, CancellationToken cancellationToken) {
			if (route.Filters == null || route.Filters.Count() == 0)
				return true;

			if (filterEvaluator == null)
				return false;

			foreach (var filter in route.Filters) {
				if (!await filterEvaluator.EvaluateAsync(message, filter, cancellationToken))
					return false;
			}

			return true;
		}

		private async Task<IList<IMessageRoute>> RetrieveRoutes(string? tenantId, string? routeId, CancellationToken cancellationToken) {
			var cacheKey = new RouteCacheKey(tenantId, routeId);

			if (!cache.TryGetValue(cacheKey, out var routes)) {
				var options = new RouteRetrievalOptions {
					TenantId = tenantId,
					RouteId = routeId
				};

				routes = await routeRetriever.ListRoutesAsync(options, cancellationToken);
				cache[cacheKey] = routes;
			}

			return routes;
		}

		struct RouteCacheKey : IEquatable<RouteCacheKey> {
			public RouteCacheKey(string? tenantId, string? routeId) {
				TenantId = tenantId;
				RouteId = routeId;
			}

			public string? TenantId { get; }

			public string? RouteId { get; }

			public override bool Equals(object? obj) {
				if (obj is RouteCacheKey key) {
					return key.TenantId == TenantId &&
					       key.RouteId == RouteId;
				}

				return false;
			}

			public bool Equals(RouteCacheKey other) => Equals((object)other);

			public override int GetHashCode() {
				return HashCode.Combine(TenantId, RouteId);
			}
		}
	}
}
