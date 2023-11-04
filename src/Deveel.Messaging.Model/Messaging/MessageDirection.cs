namespace Deveel.Messaging {
	/// <summary>
	/// The direction of a message in a queue.
	/// </summary>
	/// <remarks>
	/// The direction of a message is relative to
	/// the application that is using the queue.
	/// </remarks>
	public enum MessageDirection {
		/// <summary>
		/// The message is outbound from the queue,
		/// and it is going to be sent to a remote
		/// provider or service.
		/// </summary>
		Outbound = 1,

		/// <summary>
		/// The message is inbound to the queue,
		/// and it is coming from a remote provider
		/// </summary>
		Inbound = 2
	}
}
