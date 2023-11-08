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
		/// Gets or sets a function that is invoked when the state of
		/// the message changes.
		/// </summary>
		public Func<IMessageState, Task>? StateHandler { get; set; }
	}
}
