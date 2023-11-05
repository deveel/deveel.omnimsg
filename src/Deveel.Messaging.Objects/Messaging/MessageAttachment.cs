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
	public class MessageAttachment : IAttachment {
		public MessageAttachment(string id, string fileName, string mimeType, string content) {
			Id = id;
			FileName = fileName;
			MimeType = mimeType;
			Content = content;
		}

		public MessageAttachment() {
		}

		public MessageAttachment(IAttachment attachment) {
			Id = attachment.Id;
			FileName = attachment.FileName;
			MimeType = attachment.MimeType;
			Content = attachment.Content;
		}

		public string Id { get; set; }

		public string FileName { get; set; }

		public string MimeType { get; set; }

		[JsonConverter(typeof(Base64JsonConverter))]
		public string Content { get; set; }
	}
}
