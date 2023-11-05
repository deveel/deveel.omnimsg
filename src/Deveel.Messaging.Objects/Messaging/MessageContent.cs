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
	[JsonConverter(typeof(MessageContentJsonConverter))]
	public abstract class MessageContent : IMessageContent {
		protected MessageContent(string contentType) {
			if (string.IsNullOrWhiteSpace(contentType)) 
				throw new ArgumentException($"'{nameof(contentType)}' cannot be null or whitespace.", nameof(contentType));

			ContentType = contentType;
		}

		protected string ContentType { get; }

		string IMessageContent.ContentType => ContentType;

		public static MessageContent Create(IMessageContent content) {
			if (content is MessageContent messageContent)
				return messageContent;

			if (content is ITextContent textContent)
				return new TextContent(textContent);
			if (content is IHtmlContent htmlContent)
				return new HtmlContent(htmlContent);

			throw new NotSupportedException($"The content of type '{content.ContentType}' is not supported");
		}
	}
}
