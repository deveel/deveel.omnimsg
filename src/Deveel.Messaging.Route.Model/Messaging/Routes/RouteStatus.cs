namespace Deveel.Messaging.Routes {
	/// <summary>
	/// Enumerates the possible status of a route.
	/// </summary>
	public enum RouteStatus {
		/// <summary>
		/// The route is active and can be used.
		/// </summary>
		Active,

		/// <summary>
		/// The route is disabled and cannot be used.
		/// </summary>
		Disabled,

		/// <summary>
		/// The route has been deleted and cannot be used anymore
		/// (it will be removed from the system).
		/// </summary>
		Deleted
	}
}
