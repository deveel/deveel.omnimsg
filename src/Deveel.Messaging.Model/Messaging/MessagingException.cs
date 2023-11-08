namespace Deveel.Messaging {
	/// <summary>
	/// An exception thrown when an error occurs in the messaging system.
	/// </summary>
	public class MessagingException : Exception, IMessageError {
		/// <summary>
		/// Construts an empty exception with the given error code.
		/// </summary>
		/// <param name="errorCode">
		/// The code that identifies the error that occurred.
		/// </param>
		public MessagingException(string errorCode) {
			ErrorCode = errorCode;

			Data["ErrorCode"] = errorCode;
		}

		/// <summary>
		/// Constructs the exception with a message.
		/// </summary>
		/// <param name="errorCode">
		/// The code that identifies the error that occurred.
		/// </param>
		/// <param name="message">
		/// A message describing the error that occurred.
		/// </param>
		public MessagingException(string errorCode, string? message) 
			: base(message) {
			ErrorCode = errorCode;

			Data["ErrorCode"] = errorCode;
		}

		/// <summary>
		/// Constructs the exception with a message and an inner exception.
		/// </summary>
		/// <param name="errorCode">
		/// The code that identifies the error that occurred.
		/// </param>
		/// <param name="message">
		/// A message describing the error that occurred.
		/// </param>
		/// <param name="innerException">
		/// An inner exception that caused this exception to be thrown.
		/// </param>
		public MessagingException(string errorCode, string? message, Exception? innerException) 
			: base(message, innerException) {
			ErrorCode = errorCode;

			Data["ErrorCode"] = errorCode;
		}

		string IMessageError.Code => ErrorCode;

		/// <summary>
		/// Gets the code that identifies the error that occurred.
		/// </summary>
		public string ErrorCode { get; }

		IMessageError? IMessageError.InnerError => InnerException as IMessageError;

	}
}
