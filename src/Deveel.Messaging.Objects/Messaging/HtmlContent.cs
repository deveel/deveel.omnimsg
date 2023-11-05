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

using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Deveel.Messaging {
	/// <summary>
	/// An implementation of <see cref="IHtmlContent"/> that
	/// provides a message content that is HTML.
	/// </summary>
	public class HtmlContent : MessageContent, IHtmlContent {
		/// <summary>
		/// Constructs the content with the given HTML content,
		/// formatted as base64, and optionally a list of attachments.
		/// </summary>
		/// <param name="html">
		/// The base64 encoded HTML content of the message.
		/// </param>
		/// <param name="attachments">
		/// An optional list of attachments to the message.
		/// </param>
		public HtmlContent(string html, IEnumerable<MessageAttachment>? attachments = null) : this() {
			Html = html;
		}

		/// <summary>
		/// Constructs the content from an existing <see cref="IHtmlContent"/>
		/// that is used as a template.
		/// </summary>
		/// <param name="content">
		/// The content to use as a template.
		/// </param>
		public HtmlContent(IHtmlContent content) : this() {
			Html = content.Html;
			Attachments = content.Attachments?.Select(x => new MessageAttachment(x))?.ToList() ?? new List<MessageAttachment>();
		}

		/// <summary>
		/// Constucts an empty HTML content.
		/// </summary>
		public HtmlContent() : base(KnownMessageContentTypes.Html) {
		}

		/// <summary>
		/// Gets the HTML content of the message, as
		/// a base64 encoded string.
		/// </summary>
		[JsonConverter(typeof(Base64JsonConverter))]
		public string Html { get; set; }

		[ExcludeFromCodeCoverage]
		IEnumerable<IAttachment> IHtmlContent.Attachments => Attachments;

		/// <summary>
		/// Gets or sets the list of attachments to the message.
		/// </summary>
		public List<MessageAttachment> Attachments { get; set; }
	}
}
