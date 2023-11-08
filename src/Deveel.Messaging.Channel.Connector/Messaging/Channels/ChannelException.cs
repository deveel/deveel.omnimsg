namespace Deveel.Messaging.Channels {
	/// <summary>
	/// An exception that is thrown when an error occurs during
	/// an operation on a channel.
	/// </summary>
	public class ChannelException : MessagingException, IChannelMessageError {
		/// <summary>
		/// Constructs the exception with the given error code and type.
		/// </summary>
		/// <param name="errorCode">
		/// The code that identifies the error that occurred.
		/// </param>
		/// <param name="errorType">
		/// The type of error that occurred.
		/// </param>
		public ChannelException(string errorCode, MessageErrorType errorType) : base(errorCode) {
			ErrorType = errorType;

			Data["ErrorCode"] = errorCode;
			Data["ErrorType"] = errorType;
		}

		/// <summary>
		/// Constructs the exception with the given error code, type and message.
		/// </summary>
		/// <param name="errorCode">
		/// The code that identifies the error that occurred.
		/// </param>
		/// <param name="errorType">
		/// The type of error that occurred.
		/// </param>
		/// <param name="message">
		/// A message that describes the error that occurred.
		/// </param>
		public ChannelException(string errorCode, MessageErrorType errorType, string? message) 
			: base(errorCode, message) {
			ErrorType = errorType;

			Data["ErrorType"] = errorType;
		}

		/// <summary>
		/// Constructs the exception with the given error code, type, message and inner exception.
		/// </summary>
		/// <param name="errorCode">
		/// The code that identifies the error that occurred.
		/// </param>
		/// <param name="errorType">
		/// The type of error that occurred.
		/// </param>
		/// <param name="message">
		/// A message that describes the error that occurred.
		/// </param>
		/// <param name="innerException">
		/// The inner exception that caused this exception to be thrown.
		/// </param>
		public ChannelException(string errorCode, MessageErrorType errorType, string? message, Exception? innerException) 
			: base(errorCode, message, innerException) {
			ErrorType = errorType;

			Data["ErrorType"] = errorType;
		}

		/// <summary>
		/// Gets the type of error that occurred.
		/// </summary>
		public MessageErrorType ErrorType { get; }
	}
}
