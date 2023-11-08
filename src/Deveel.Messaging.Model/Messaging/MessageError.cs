namespace Deveel.Messaging {
	/// <summary>
	/// A structure that provides information about an error
	/// that occurred while processing a message.
	/// </summary>
	public readonly struct MessageError : IMessageError {
		/// <summary>
		/// Constructs the error with the given <paramref name="code"/>
		/// and an optional <paramref name="message"/>.
		/// </summary>
		/// <param name="code">
		/// The code that identifies the error.
		/// </param>
		/// <param name="message">
		/// A message that describes the error.
		/// </param>
		public MessageError(string code, string? message, IMessageError? innerError) : this() {
			Code = code;
			Message = message;
			InnerError = innerError;
		}

		/// <inheritdoc />
		public string Code { get; }

		/// <inheritdoc />
		public string? Message { get; }

		/// <inheritdoc />
		public IMessageError? InnerError { get; }
	}
}
