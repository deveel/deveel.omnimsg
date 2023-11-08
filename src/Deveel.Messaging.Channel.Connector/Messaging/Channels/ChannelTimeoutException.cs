namespace Deveel.Messaging.Channels {
	public sealed class ChannelTimeoutException : ChannelTransientException {
		public ChannelTimeoutException() 
			: base("channel.timeout") {
		}

		public ChannelTimeoutException(string? message) 
			: base("channel.timeout", message) {
		}

		public ChannelTimeoutException(string? message, Exception? innerException) 
			: base("channel.timeout", message, innerException) {
		}
	}
}
