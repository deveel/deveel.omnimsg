namespace Deveel.Messaging {
	/// <summary>
	/// Lists the known content types of a message.
	/// </summary>
	/// <remarks>
	/// This list is not to be an enumeration and is
	/// suitable to be extended by the user, if new types
	/// of content are available.
	/// </remarks>
	public static class KnownMessageContentTypes {
		/// <summary>
		/// A plain text content.
		/// </summary>
		public const string Text = "text";

		/// <summary>
		/// A content that is encoded in HTML.
		/// </summary>
		public const string Html = "html";

		/// <summary>
		/// A content that is composed by a set of parts.
		/// </summary>
		public const string Multipart = "multipart";

		/// <summary>
		/// A content that is composed by media file.
		/// </summary>
		public const string Media = "media";
	}
}
