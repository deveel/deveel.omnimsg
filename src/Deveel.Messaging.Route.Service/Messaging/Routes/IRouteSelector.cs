using Deveel.Messaging.Routes;

namespace Deveel.Messaging.Routes {
	/// <summary>
	/// A service that is able to select a route
	/// that matches the given message.
	/// </summary>
	public interface IRouteSelector {
		/// <summary>
		/// Attempts to select the first route that
		/// matches the given message.
		/// </summary>
		/// <param name="message">
		/// A message that has to be handled by the route.
		/// </param>
		/// <param name="cancellationToken">
		/// A token used to cancel the selection of the route.
		/// </param>
		/// <returns>
		/// Returns the first instance of <see cref="IMessageRoute"/> 
		/// that matches the given message, or <c>null</c> if no route
		/// was found that can handle the message.
		/// </returns>
		Task<IMessageRoute?> SelectRouteAsync(IMessage message, CancellationToken cancellationToken = default);

		/// <summary>
		/// Attempts to select a route by the given identifier.
		/// </summary>
		/// <param name="tenantId">
		/// The unique identifier of the tenant that owns the route.
		/// </param>
		/// <param name="routeId">
		/// The unique identifier of the route to select.
		/// </param>
		/// <param name="cancellationToken">
		/// A token used to cancel the selection of the route.
		/// </param>
		/// <returns>
		/// Returns an instance of <see cref="IMessageRoute"/> that
		/// is identified by the given identifier, or <c>null</c> if
		/// the route was not found.
		/// </returns>
		Task<IMessageRoute?> SelectRouteByIdAsync(string? tenantId, string routeId, CancellationToken cancellationToken = default);
	}
}