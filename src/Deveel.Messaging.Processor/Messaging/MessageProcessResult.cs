namespace Deveel.Messaging {
	/// <summary>
	/// A structure that represents the result of a message processing.
	/// </summary>
	public readonly struct MessageProcessResult {
		private MessageProcessResult(IMessage message, bool processed, bool successful, string? errorCode, string? errorMessage) {
			Message = message;
			Processed = processed;
			Successful = successful;
			ErrorCode = errorCode;
			ErrorMessage = errorMessage;
		}

		/// <summary>
		/// Gets the message that has been processed.
		/// </summary>
		public IMessage Message { get; }

		/// <summary>
		/// Gets a value indicating if the message has been processed.
		/// </summary>
		public bool Processed { get; }

		/// <summary>
		/// When the message has been processed, gets a value indicating
		/// if the processing was successful.
		/// </summary>
		public bool Successful { get; }

		/// <summary>
		/// When the message has been processed, gets the error code
		/// that has been raised.
		/// </summary>
		public string? ErrorCode { get; }

		/// <summary>
		/// When the message has been processed, gets the error message
		/// that describes the error.
		/// </summary>
		public string? ErrorMessage { get; }

		/// <summary>
		/// Creates a new successful result of a message processing.
		/// </summary>
		/// <param name="message">
		/// The message that has been processed.
		/// </param>
		/// <remarks>
		/// In some transformation processes, the message that is returned
		/// from this method may be different from the one passed as argument
		/// of the processor.
		/// </remarks>
		/// <returns>
		/// Returns a new instance of <see cref="MessageProcessResult"/>
		/// that represents a successful processing of a message.
		/// </returns>
		public static MessageProcessResult Success(IMessage message) {
			return new MessageProcessResult(message, true, true, null, null);
		}

		/// <summary>
		/// Creates a new result of a message processing that has failed.
		/// </summary>
		/// <param name="message">
		/// The message that has been processed.
		/// </param>
		/// <param name="errorCode">
		/// A unique code that identifies the error.
		/// </param>
		/// <param name="errorMessage">
		/// An optional message that describes the error.
		/// </param>
		/// <returns>
		/// Returns a new instance of <see cref="MessageProcessResult"/>
		/// that represents a failed processing of a message.
		/// </returns>
		public static MessageProcessResult Failed(IMessage message, string errorCode, string? errorMessage = null) {
			return new MessageProcessResult(message, true, false, errorCode, errorMessage);
		}

		/// <summary>
		/// Creates a new result that indicates that the message has not
		/// been processed.
		/// </summary>
		/// <param name="message">
		/// The message that was the argument of the processor.
		/// </param>
		/// <returns>
		/// Returns a new instance of <see cref="MessageProcessResult"/>
		/// that indicates that the message has not been processed.
		/// </returns>
		public static MessageProcessResult Unprocessed(IMessage message) {
			return new MessageProcessResult(message, false, false, null, null);
		}
	}
}
