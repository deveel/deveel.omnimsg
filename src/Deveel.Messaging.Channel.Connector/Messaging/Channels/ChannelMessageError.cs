namespace Deveel.Messaging.Channels {
	public sealed class ChannelMessageError : IChannelMessageError {
		private ChannelMessageError(string code, string? message, MessageErrorType errorType, IMessageError? innerError) {
			ErrorType = errorType;
			Code = code;
			Message = message;
			InnerError = innerError;
		}

		public MessageErrorType ErrorType { get; }

		public string Code { get; }

		public string? Message { get; }

		public IMessageError? InnerError { get; }

		public static ChannelMessageError FromException(ChannelException error)
			=> new ChannelMessageError(error.ErrorCode, error.Message, error.ErrorType, (error.InnerException is ChannelException inner) ? FromException(inner) : null);

		public static ChannelMessageError Transient(string errorCode, string? errorMessage = null, IMessageError? innerError = null)
			=> new ChannelMessageError(errorCode, errorMessage, MessageErrorType.Transient, innerError);

		public static ChannelMessageError Terminal(string errorCode, string? errorMessage = null, IMessageError? innerError = null)
			=> new ChannelMessageError(errorCode, errorMessage, MessageErrorType.Terminal, innerError);

		internal static List<ChannelMessageError>? NormalizeErrors(IEnumerable<IChannelMessageError>? errors)
			=> errors?.Select(x => x.ErrorType == MessageErrorType.Terminal ? ChannelMessageError.Terminal(x.Code, x.Message) : ChannelMessageError.Transient(x.Code, x.Message))?.ToList();
	}
}
