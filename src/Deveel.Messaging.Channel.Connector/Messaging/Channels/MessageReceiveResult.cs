namespace Deveel.Messaging.Channels {
	/// <summary>
	/// The result of a message receiving operation.
	/// </summary>
	public readonly struct MessageReceiveResult {
		private MessageReceiveResult(bool isSuccess, IMessage? message = null, IReadOnlyList<ChannelMessageError>? errors = null) {
			IsSuccess = isSuccess;
			Message = message;
			Errors = errors;
		}

		/// <summary>
		/// Gets the instance of the received message.
		/// </summary>
		public IMessage? Message { get; }

		/// <summary>
		/// Gets a value indicating if the receiving operation
		/// was successful.
		/// </summary>
		public bool IsSuccess { get; }

		/// <summary>
		/// If the receiving operation was not successful, gets
		/// a list of errors that have been raised.
		/// </summary>
		public IReadOnlyList<ChannelMessageError>? Errors { get; }

		/// <summary>
		/// Constructs a successful result of a message receiving
		/// operation.
		/// </summary>
		/// <param name="message">
		/// The instance of the received message.
		/// </param>
		/// <returns>
		/// Returns an instance of <see cref="MessageReceiveResult"/>
		/// that represents a successful receiving operation.
		/// </returns>
		public static MessageReceiveResult Success(IMessage message)
			=> new MessageReceiveResult(true, message);

		/// <summary>
		/// Constructs a failed result of a message receiving operation.
		/// </summary>
		/// <param name="errors">
		/// A list of errors that have been raised during the receiving
		/// operation.
		/// </param>
		/// <returns>
		/// Returns an instance of <see cref="MessageReceiveResult"/>
		/// that represents a failed receiving operation.
		/// </returns>
		public static MessageReceiveResult Failed(params IChannelMessageError[] errors)
			=> new MessageReceiveResult(false, errors: ChannelMessageError.NormalizeErrors(errors));

		/// <summary>
		/// Constructs a failed result of a message receiving operation.
		/// </summary>
		/// <param name="errors">
		/// The list of errors that have been raised during the receiving operation.
		/// </param>
		/// <returns>
		/// Returns an instance of <see cref="MessageReceiveResult"/>
		/// that represents a failed receiving operation.
		/// </returns>
		public static MessageReceiveResult Failed(IEnumerable<IChannelMessageError> errors)
			=> new MessageReceiveResult(false, errors: ChannelMessageError.NormalizeErrors(errors));

		public static MessageReceiveResult Failed(IChannelMessageError error)
			=> new MessageReceiveResult(false, errors: ChannelMessageError.NormalizeErrors(new[] { error }));

		// Messages while receiving messages are always terminal
		public static MessageReceiveResult Failed(string code, string? message = null)
			=> new MessageReceiveResult(false, errors: new[] { ChannelMessageError.Terminal(code, message) });
	}
}