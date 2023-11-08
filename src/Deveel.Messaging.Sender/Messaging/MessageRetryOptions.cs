namespace Deveel.Messaging {
	/// <summary>
	/// Configures the options for retrying the sending
	/// of a message.
	/// </summary>
	public sealed class MessageRetryOptions {
		/// <summary>
		/// Gets or sets the strategy to use to retry
		/// the sending of a message.
		/// </summary>
		public MessageRetryStrategy? Strategy { get; set; }

		/// <summary>
		/// Gets or sets the delay to wait before retrying
		/// the sending of a message.
		/// </summary>
		public TimeSpan? Delay { get; set; }

		/// <summary>
		/// Gets or sets the default number of times 
		/// to retry the sending of a message.
		/// </summary>
		public int? MaxRetries { get; set; }
	}
}
