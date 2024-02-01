namespace Deveel.Messaging.Routes {
	/// <summary>
	/// A default implementation of <see cref="IMessageRoute"/>.
	/// </summary>
	public class MessageRoute : IMessageRoute {
		/// <summary>
		/// Constructs an empty route.
		/// </summary>
		public MessageRoute() {
		}

		/// <summary>
		/// Constructs a route from the given <paramref name="route"/>.
		/// </summary>
		/// <param name="route"></param>
		public MessageRoute(IMessageRoute route) {
			Id = route.Id;
			Name = route.Name;
			TenantId = route.TenantId;
			Priority = route.Priority;
			Direction = route.Direction;
			Channel = new RouteChannel(route.Channel);
			Filters = route.Filters?.Select(RouteFilter.Create).ToList() ?? new List<RouteFilter>();
		}

		/// <inheritdoc/>
		public string Id { get; set; }

		/// <inheritdoc/>
		public string Name { get; set; }

		/// <inheritdoc/>
		public string? TenantId { get; set; }

		/// <inheritdoc/>
		public int Priority { get; set; }

		/// <inheritdoc/>
		public RoutingDirection Direction { get; set; }

		IRouteChannel IMessageRoute.Channel => Channel;

		/// <inheritdoc/>
		public RouteChannel Channel { get; set; }

		IEnumerable<IRouteFilter> IMessageRoute.Filters => Filters;

		/// <inheritdoc/>
		public List<RouteFilter> Filters { get; set; }
	}
}