namespace Deveel.Messaging.Routes {
	/// <summary>
	/// An expression used to filter a message to
	/// select the route to be used.
	/// </summary>
	public interface IExpressionRouteFilter : IRouteFilter {
		/// <summary>
		/// The expression string that is used 
		/// to select the route.
		/// </summary>
		string Expression { get; }

		/// <summary>
		/// The language used to parse the expression.
		/// </summary>
		string? Language { get; }
	}
}
