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

using System.Text.Json.Serialization;

namespace Deveel.Messaging {
	/// <summary>
	/// Represents a plain-text content of a message.
	/// </summary>
	public class TextContent : MessageContent, ITextContent {
		/// <summary>
		/// Constructs the content with the given text and encoding.
		/// </summary>
		/// <param name="text">
		/// The text content of the message.
		/// </param>
		/// <param name="encoding">
		/// An optional code that specifies the encoding of the text.
		/// </param>
		public TextContent(string? text, string? encoding = null) : this() {
			Encoding = encoding;
			Text = text;
		}

		/// <summary>
		/// Constructs the content with no text and no encoding.
		/// </summary>
		public TextContent() : base(KnownMessageContentTypes.Text) {
		}

		/// <summary>
		/// Constructs the content from the given instance.
		/// </summary>
		/// <param name="content">
		/// The source instance of <see cref="ITextContent"/> that is used
		/// to initialize the properties of this instance.
		/// </param>
		public TextContent(ITextContent content)
			: this(content.Text, content.Encoding) { 
		}

		/// <inheritdoc/>
		public string? Encoding { get; set; }

		/// <inheritdoc/>
		public string? Text { get; set; }

		//protected override string ContentType => KnownMessageContentTypes.Text;
	}
}
