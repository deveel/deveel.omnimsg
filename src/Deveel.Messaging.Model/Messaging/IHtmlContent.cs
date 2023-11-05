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
