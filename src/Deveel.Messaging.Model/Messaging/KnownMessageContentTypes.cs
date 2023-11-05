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
