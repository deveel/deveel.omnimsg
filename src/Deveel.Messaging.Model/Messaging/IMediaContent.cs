// Copyright 2023 Deveel AS
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace Deveel.Messaging {
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
