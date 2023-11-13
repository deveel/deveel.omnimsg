namespace Deveel.Messaging {
	/// <summary>
	/// Configures the default options for a <see cref="MessageSender"/>.
	/// </summary>
	public class MessageSenderOptions {
		/// <summary>
		/// Gets or sets the options for the retry policy of the sender.
		/// </summary>
		public MessageRetryOptions Retry { get; set; } = new MessageRetryOptions();

		/// <summary>
		/// Gets or sets the default timeout (in milliseconds) for 
		/// sending a message.
		/// </summary>
		public int? SendTimeout { get; set; }

		/// <summary>
		/// Gets or sets a flag indicating if the sender should log
		/// the messages sent.
		/// </summary>
		/// <remarks>
		/// The initial point where to log the messages depends on the
		/// implementation of the service, and it may be the moment 
		/// that the message is sent, or when it is received by the
		/// service from a third party, to be route it through the sender.
		/// </remarks>
		public bool LogMessages { get; set; } = true;

		/// <summary>
		/// Gets or sets a function that is invoked when the state of
		/// the message changes.
		/// </summary>
		public Func<IMessageState, Task>? StateHandler { get; set; }
	}
}
