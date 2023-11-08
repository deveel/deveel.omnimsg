namespace Deveel.Messaging.Channels {
	public class ChannelTransientException : ChannelException {
		public ChannelTransientException(string errorCode) 
			: base(errorCode, MessageErrorType.Transient) {
		}

		public ChannelTransientException(string errorCode, string? message) 
			: base(errorCode, MessageErrorType.Transient, message) {
		}

		public ChannelTransientException(string errorCode, string? message, Exception? innerException) 
			: base(errorCode, MessageErrorType.Transient, message, innerException) {
		}
	}
}
