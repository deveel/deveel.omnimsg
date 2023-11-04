namespace Deveel.Messaging {
	/// <summary>
	/// The content that is transported by a message.
	/// </summary>
	public interface IMessageContent {
		/// <summary>
		/// Gets the identifier of the type of content.
		/// </summary>
		/// <remarks>
		/// The value returned by this property
		/// is not a fully qualified MIME type,
		/// and it depends on the protocol used.
		/// </remarks>
		string ContentType { get; }
	}
}
