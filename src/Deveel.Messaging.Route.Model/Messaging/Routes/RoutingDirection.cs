namespace Deveel.Messaging.Routes {
	/// <summary>
	/// Describes the direction of a message route.
	/// </summary>
	public enum RoutingDirection {
		/// <summary>
		/// The route is inbound, meaning that it is able to
		/// transport messages from an external source to the
		/// application.
		/// </summary>
		Inbound = 1,

		/// <summary>
		/// The route is outbound, meaning that it is able to
		/// transport messages from the application to an
		/// external destination.
		/// </summary>
		Outbound = 2
	}
}
