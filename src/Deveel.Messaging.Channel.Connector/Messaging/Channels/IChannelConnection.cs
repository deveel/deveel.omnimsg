namespace Deveel.Messaging.Channels {
	/// <summary>
	/// Represents a connection to a channel, for sending and receiving
	/// messages.
	/// </summary>
	public interface IChannelConnection : IDisposable {
		/// <summary>
		/// Gets a value indicating if the connection is active and
		/// it is possible to send and receive messages.
		/// </summary>
		bool IsConnected { get; }

		/// <summary>
		/// Creates a service that can be used to send messages 
		/// to the channel.
		/// </summary>
		/// <returns>
		/// Returns an instance of <see cref="IChannelSender"/> that can be
		/// used to send messages to the channel.
		/// </returns>
		IChannelSender CreateSender();

		/// <summary>
		/// Creates a service that can be used to receive messages
		/// from the channel.
		/// </summary>
		/// <returns>
		/// Returns an instance of <see cref="IChannelReceiver"/> that is
		/// specific to the channel and can be used to receive messages.
		/// </returns>
		IChannelReceiver CreateReceiver();
	}
}
