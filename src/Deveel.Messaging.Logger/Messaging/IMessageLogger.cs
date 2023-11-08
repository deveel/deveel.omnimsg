namespace Deveel.Messaging {
	/// <summary>
	/// A service that is used to log messages and their states.
	/// </summary>
	public interface IMessageLogger {
		/// <summary>
		/// Logs a message that has been sent or received.
		/// </summary>
		/// <param name="message">
		/// The message object to log in the underlying storage.
		/// </param>
		/// <param name="cancellationToken">
		/// A cancellation token that can be used to cancel the operation.
		/// </param>
		/// <returns>
		/// Returns a task that when completed will log
		/// the message in the underlying storage.
		/// </returns>
		Task LogMessageAsync(IMessage message, CancellationToken cancellationToken = default);

		/// <summary>
		/// Logs a status update of a message that was sent or received.
		/// </summary>
		/// <param name="state">
		/// The state object to log in the underlying storage.
		/// </param>
		/// <param name="cancellationToken">
		/// A cancellation token that can be used to cancel the operation.
		/// </param>
		/// <returns>
		/// Returns a task that when completed will log
		/// the message state in the underlying storage.
		/// </returns>
		Task LogMessageStateAsync(IMessageState state, CancellationToken cancellationToken = default);
	}
}
