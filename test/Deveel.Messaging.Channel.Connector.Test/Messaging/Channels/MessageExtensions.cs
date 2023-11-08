namespace Deveel.Messaging.Channels {
	public static class MessageExtensions {
		public static IMessage WithTestError(this IMessage message, IMessageError error) {
			if (message == null)
				throw new ArgumentNullException(nameof(message));
			if (error == null)
				throw new ArgumentNullException(nameof(error));

			return message.With(new Dictionary<string, object> {
				{ "test.error.code", error.Code },
				{ "test.error.type", (error as IChannelMessageError)?.ErrorType ?? MessageErrorType.Terminal },
				{ "test.error.message", error.Message ?? "" }
			});
		}

		public static IMessageError? TestError(this IMessage message) {
			if ((message.Properties?.TryGetValue("test.error.code", out var code) ?? false) &&
				(message.Properties?.TryGetValue("test.error.type", out var type) ?? false)) {
				var errorType = Enum.Parse<MessageErrorType>(type.ToString()!);
				var errorMessage = message.Properties.TryGetValue("test.error.message", out var messageValue)
					? messageValue.ToString()
					: null;

				return errorType == MessageErrorType.Transient ?
					ChannelMessageError.Transient(code.ToString()!, errorMessage) :
					ChannelMessageError.Terminal(code.ToString()!, errorMessage);
			}

			return null;
		}
	}
}
