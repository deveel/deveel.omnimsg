namespace Deveel.Messaging.Channels {
	/// <summary>
	/// Describes an error that occurred while sending or receiving
	/// a message through a channel.
	/// </summary>
	public interface IChannelMessageError : IMessageError {
		/// <summary>
		/// Gets the type of error that occurred.
		/// </summary>
		MessageErrorType ErrorType { get; }
	}
}
