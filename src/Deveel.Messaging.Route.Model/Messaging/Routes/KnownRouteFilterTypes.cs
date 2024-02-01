namespace Deveel.Messaging.Routes {
	/// <summary>
	/// A list of the known types of <see cref="IRouteFilter"/>
	/// </summary>
	public static class KnownRouteFilterTypes {
		/// <summary>
		/// An empty filter that does not perform any operation.
		/// </summary>
		public const string Empty = "empty";

		/// <summary>
		/// A filter that performs evaluates the given expression
		/// against a message to select a route.
		/// </summary>
		public const string Expression = "expression";
	}
}
