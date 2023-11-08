namespace Deveel.Messaging {
	/// <summary>
	/// The types of retry strategies that can be used 
	/// when sending a message.
	/// </summary>
	public enum MessageRetryStrategy {
		/// <summary>
		/// A retry strategy that uses an exponential backoff
		/// for the delay between each retry.
		/// </summary>
		ExponentialBackoff,

		/// <summary>
		/// A retry strategy that uses a linear backoff
		/// for the delay between each retry.
		/// </summary>
		LinearBackoff,

		/// <summary>
		/// A constant retry strategy that uses a fixed
		/// time delay between each retry.
		/// </summary>
		Constant
	}
}
