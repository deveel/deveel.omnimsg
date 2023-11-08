namespace Deveel.Messaging {
	/// <summary>
	/// A service that allows to store messages in
	/// a temporary store, to be processed later.
	/// </summary>
	public interface IMessageCache {
		/// <summary>
		/// Attempts to retrieve a message by its identifier.
		/// </summary>
		/// <param name="messageId">
		/// The identifier of the message to retrieve.
		/// </param>
		/// <param name="cancellationToken">
		/// A token that can be used to cancel the operation.
		/// </param>
		/// <returns>
		/// Returns an instance of <see cref="IMessage"/> that has
		/// been retrieved from the cache, or <c>null</c> if no message
		/// was found with the given identifier.
		/// </returns>
		Task<IMessage?> GetByIdAsync(string messageId, CancellationToken cancellationToken = default);

		/// <summary>
		/// Sets a message in the cache, using the identifier
		/// and configuration provided by the message.
		/// </summary>
		/// <param name="message">
		/// The message to set in the cache.
		/// </param>
		/// <remarks>
		/// The message instance should provide the identifier
		/// for forming the key in the cache, and the configuration
		/// to determine the expiration of the message.
		/// </remarks>
		/// <param name="cancellationToken">
		/// A token that can be used to cancel the operation.
		/// </param>
		/// <returns>
		/// Returns a task that will complete when the message
		/// has been set in the cache.
		/// </returns>
		Task SetAsync(IMessage message, CancellationToken cancellationToken = default);

		/// <summary>
		/// Removes a message from the cache.
		/// </summary>
		/// <param name="messageId">
		/// The identifier of the message to remove.
		/// </param>
		/// <param name="cancellationToken">
		/// A token that can be used to cancel the operation.
		/// </param>
		/// <returns>
		/// Returns a task that will complete when the message
		/// has been removed from the cache.
		/// </returns>
		Task RemoveAsync(string messageId, CancellationToken cancellationToken = default);
	}
}
