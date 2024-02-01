namespace Deveel.Messaging.Routes {
	/// <summary>
	/// A client service that is able to resolve a route
	/// given some conditions.
	/// </summary>
	public interface IMessageRouteResolver {
		/// <summary>
		/// Attempts to find a route by the given identifier.
		/// </summary>
		/// <param name="id">
		/// The identifier of the route to find.
		/// </param>
		/// <param name="options">
		/// An optional set of options to use to resolve the route.
		/// </param>
		/// <param name="cancellationToken">
		/// A cancellation token that can be used to cancel the
		/// resolution of the route.
		/// </param>
		/// <returns>
		/// Returns an instance of <see cref="IMessageRoute"/> that
		/// was found by the given identifier, or <c>null</c> if no
		/// route was found.
		/// </returns>
		Task<IMessageRoute?> FindByIdAsync(string id, RouteResolutionOptions? options = null, CancellationToken cancellationToken = default);

		/// <summary>
		/// Attempts to resolve the first route that 
		/// matches the given message.
		/// </summary>
		/// <param name="message">
		/// The message that has to be handled by the route.
		/// </param>
		/// <param name="options">
		/// An optional set of options to use to resolve the route.
		/// </param>
		/// <param name="cancellationToken">
		/// A cancellation token that can be used to cancel the
		/// resolution of the route.
		/// </param>
		/// <returns>
		/// Returns an instance of <see cref="IMessageRoute"/> that
		/// is able to handle the given message, or <c>null</c> if no
		/// route was found that can handle the message.
		/// </returns>
		Task<IRouteChannel?> ResolveChannelAsync(IMessage message, RouteResolutionOptions? options = null, CancellationToken cancellationToken = default);
	}
}
