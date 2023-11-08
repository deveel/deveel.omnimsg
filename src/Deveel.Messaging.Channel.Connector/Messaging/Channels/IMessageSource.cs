namespace Deveel.Messaging.Channels {
	/// <summary>
	/// Represents a source of messages that can be received
	/// from a channel.
	/// </summary>
	public interface IMessageSource {
		/// <summary>
		/// Gets the format of the message that is expected
		/// to be received from the source.
		/// </summary>
		string MessageFormat { get; }
	}
}
