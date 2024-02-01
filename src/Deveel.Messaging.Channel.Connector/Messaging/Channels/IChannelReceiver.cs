namespace Deveel.Messaging.Channels {
	/// <summary>
	/// A service that is able to receive messages from the
	/// channel provider.
	/// </summary>
	public interface IChannelReceiver {
		/// <summary>
		/// Gets a value indicating if the receiver is currently
		/// receiving messages from the channel.
		/// </summary>
		bool IsReceiving { get; }

		/// <summary>
		/// Waits until a message is received from the channel.
		/// </summary>
		/// <param name="cancellationToken">
		/// A token that can be used to cancel the operation.
		/// </param>
		/// <returns>
		/// Returns an instance of <see cref="IMessage"/> that represents
		/// a message received from the channel.
		/// </returns>
		Task<MessageReceiveResult> ReceiveAsync(CancellationToken cancellationToken = default);
	}
}
