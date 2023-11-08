namespace Deveel.Messaging {
	/// <summary>
	/// A service that handles the state of a message.
	/// </summary>
	public interface IMessageStateHandler {
		/// <summary>
		/// Handles the state of a message.
		/// </summary>
		/// <param name="state">
		/// The message state to handle.
		/// </param>
		/// <param name="cancellationToken">
		/// A cancellation token that can be used to cancel the operation.
		/// </param>
		/// <returns>
		/// Returns a task that when completed will handle
		/// the state of the message.
		/// </returns>
		Task HandleAsync(IMessageState state, CancellationToken cancellationToken = default);
	}
}
