namespace Deveel.Messaging.Channels {
	public class ChannelTerminalException : ChannelException {
		public ChannelTerminalException(string errorCode) 
			: base(errorCode, MessageErrorType.Terminal) {
		}

		public ChannelTerminalException(string errorCode, string? message) 
			: base(errorCode, MessageErrorType.Transient, message) {
		}

		public ChannelTerminalException(string errorCode, string? message, Exception? innerException) 
			: base(errorCode, MessageErrorType.Transient, message, innerException) {
		}
	}
}
