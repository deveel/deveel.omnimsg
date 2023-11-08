namespace Deveel.Messaging.Channels {
	/// <summary>
	/// A service that is able to send messages to the 
	/// provider of the channel.
	/// </summary>
	public interface IChannelSender {
		/// <summary>
		/// Sends a message through the underlying channel.
		/// </summary>
		/// <param name="message">
		/// The message object to be sent.
		/// </param>
		/// <param name="cancellationToken">
		/// A token that can be used to cancel the operation.
		/// </param>
		/// <returns>
		/// Returns an instance of <see cref="MessageResult"/> that provides
		/// information about the result of the sending operation.
		/// </returns>
		Task<MessageResult> SendAsync(IMessage message, CancellationToken cancellationToken = default);
	}
}
