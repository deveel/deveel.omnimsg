namespace Deveel.Messaging.Routes {
	/// <summary>
	/// A service that is used to retrieve all the
	/// routes in a messaging application
	/// </summary>
	public interface IRouteRetriever {
		/// <summary>
		/// Lists all routes in an application.
		/// </summary>
		/// <param name="options">
		/// An optional set of options used to filter the
		/// routes to retrieve.
		/// </param>
		/// <param name="cancellationToken">
		/// A token used to cancel the retrieval operation.
		/// </param>
		/// <returns>
		/// Returns a list of all the <see cref="IMessageRoute"/>
		/// instance available in an application context
		/// </returns>
		Task<IList<IMessageRoute>> ListRoutesAsync(RouteRetrievalOptions? options = null, CancellationToken cancellationToken = default);
	}
}
