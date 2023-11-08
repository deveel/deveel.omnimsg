namespace Deveel.Messaging.Channels {
	/// <summary>
	/// Enumerates the types of errors that can occur
	/// while sending a message.
	/// </summary>
	public enum MessageErrorType {
		/// <summary>
		/// A transient error that should disappear after
		/// a certain amount of time.
		/// </summary>
		Transient = 1,

		/// <summary>
		/// An error that is terminal and cannot be recovered.
		/// </summary>
		Terminal = 2
	}
}
