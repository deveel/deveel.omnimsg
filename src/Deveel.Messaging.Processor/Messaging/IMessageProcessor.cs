namespace Deveel.Messaging {
	/// <summary>
	/// A service that processes a message, to apply some logic
	/// and transformations.
	/// </summary>
	public interface IMessageProcessor {
		/// <summary>
		/// Processes the given message.
		/// </summary>
		/// <param name="message">
		/// The instance of the message to process.
		/// </param>
		/// <param name="cancellationToken">
		/// A token that can be used to cancel the processing.
		/// </param>
		/// <returns>
		/// Returns an instance of <see cref="MessageProcessResult"/> that
		/// describes the result of the processing.
		/// </returns>
		Task<MessageProcessResult> ProcessAsync(IMessage message, CancellationToken cancellationToken = default);
	}
}
