namespace Deveel.Messaging {
	/// <summary>
	/// A type of message that contains plain-text.
	/// </summary>
	public interface ITextContent : IMessageContent {
		/// <summary>
		/// Gets the code of the encoding used to 
		/// encode the text.
		/// </summary>
		/// <remarks>
		/// When this is not specified, the default
		/// channel encoding is used.
		/// </remarks>
		string? Encoding { get; }

		/// <summary>
		/// Gets the text of the message.
		/// </summary>
		string? Text { get; }
	}
}
