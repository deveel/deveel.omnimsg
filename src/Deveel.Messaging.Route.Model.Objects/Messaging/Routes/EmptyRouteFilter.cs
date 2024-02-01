namespace Deveel.Messaging.Routes {
	/// <summary>
	/// A default implementation of an empty <see cref="RouteFilter"/>.
	/// </summary>
	public sealed class EmptyRouteFilter : RouteFilter {
		/// <inheritdoc/>
		protected override string FilterType => KnownRouteFilterTypes.Empty;
	}
}
