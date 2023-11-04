﻿namespace Deveel.Messaging {
	/// <summary>
	/// Represents a content of a message that is
	/// a media file.
	/// </summary>
	public interface IMediaContent : IMessageContent {
		/// <summary>
		/// Gets the type of media that is represented
		/// by the content.
		/// </summary>
		string MediaType { get; }

		/// <summary>
		/// Gets the name of the file that is attached.
		/// </summary>
		string? FileName { get; }

		/// <summary>
		/// Gets the location of the file that is attached.
		/// </summary>
		/// <remarks>
		/// This is an optional property that can be used
		/// alternatively to <see cref="Data"/>.
		/// </remarks>
		string? FileUrl { get; }

		/// <summary>
		/// Gets the binary data of the media content,
		/// encoded in base64.
		/// </summary>
		/// <remarks>
		/// This is an optional property that can be used
		/// alternatively to <see cref="FileUrl"/>.
		/// </remarks>
		string? Data { get; }
	}
}
