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
	/// An attachment to a message that has
	/// another type of content.
	/// </summary>
	public interface IAttachment {
		/// <summary>
		/// Gets the unique identifier of the attachment
		/// within the scope of the message.
		/// </summary>
		string Id { get; }

		/// <summary>
		/// Gets the name of the file that is attached.
		/// </summary>
		string FileName { get; }

		/// <summary>
		/// Gets the MIME type of the file that is attached.
		/// </summary>
		string MimeType { get; }

		/// <summary>
		/// Gets a base64-encoded string that represents
		/// the content of the attachment.
		/// </summary>
		string Content { get; }
	}
}
