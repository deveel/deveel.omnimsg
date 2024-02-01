namespace Deveel.Messaging.Routes {
	/// <summary>
	/// Represents a message route that is used to
	/// determine how a message is routed to a channel.
	/// </summary>
	public interface IMessageRoute {
		/// <summary>
		/// Gets the unique identifier of the route.
		/// </summary>
		string Id { get; }

		/// <summary>
		/// Gets the name of the route.
		/// </summary>
		string Name { get; }

		/// <summary>
		/// Gets the identifier of the tenant that owns
		/// the route.
		/// </summary>
		string? TenantId { get; }

		/// <summary>
		/// Gets the priority of the route in the routing table.
		/// </summary>
		int Priority { get; }

		/// <summary>
		/// Gets the routing direction of messages.
		/// </summary>
		RoutingDirection Direction { get; }

		/// <summary>
		/// Gets the channel that is used to route a message
		/// that matches the route.
		/// </summary>
		IRouteChannel Channel { get; }

		/// <summary>
		/// Gets a collection of filters that are used to
		/// match a message to the route.
		/// </summary>
		IEnumerable<IRouteFilter> Filters { get; }
	}
}