namespace Deveel.Messaging.Channels {
	public readonly struct MessageResult {
		private MessageResult(IMessage? message, bool success, IMessageError? error) {
			Message = message;
			Successful = success;
			Error = error;
		}

		public IMessage? Message { get; }

		public bool Successful { get; }

		public IMessageError? Error { get; }

		public static MessageResult Success(IMessage message) => new MessageResult(message, true, null);

		public static MessageResult Fail(IMessageError error) {
			return new MessageResult(null, false, error);
		}

		public static MessageResult Fail(MessagingException error)
			=> Fail(new MessageError(error.ErrorCode, error.Message, error.InnerException as IMessageError));

		public static MessageResult Fail(string errorCode, string? errorMessage = null, IMessageError? innerError = null)
			=> Fail(new MessageError(errorCode, errorMessage, innerError));
	}
}
