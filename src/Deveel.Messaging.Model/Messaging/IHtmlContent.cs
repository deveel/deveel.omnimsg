﻿namespace Deveel.Messaging {
	/// <summary>
	/// A content that is encoded in HTML.
	/// </summary>
	public interface IHtmlContent : IMessageContent {
		/// <summary>
		/// Gets the base64 encoded HTML content.
		/// </summary>
		string Html { get; }

		/// <summary>
		/// Gets a set of optional attachments that are
		/// included to the message content.
		/// </summary>
		IEnumerable<IAttachment> Attachments { get; }
	}
}
