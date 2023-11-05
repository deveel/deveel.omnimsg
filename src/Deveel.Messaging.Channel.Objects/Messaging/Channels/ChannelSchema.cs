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

namespace Deveel.Messaging.Channels {
	public sealed class ChannelSchema : IChannelSchema {
		public ChannelSchema() {
		}

		public string Type { get; set; }

		public string Provider { get; set; }

		public ChannelDirection Directions { get; set; }

		IEnumerable<string> IChannelSchema.AllowedSenderTypes => AllowedSenderTypes;

		public IList<string> AllowedSenderTypes { get; set; } = new List<string>();

		IEnumerable<string> IChannelSchema.RequiredSenderTypes => RequiredSenderTypes;

		public IList<string> RequiredSenderTypes { get; set; } = new List<string>();

		IEnumerable<string> IChannelSchema.AllowedReceiverTypes => AllowedReceiverTypes;

		public IList<string> AllowedReceiverTypes { get; set; } = new List<string>();

		IEnumerable<string> IChannelSchema.RequiredReceiverTypes => RequiredReceiverTypes;

		public IList<string> RequiredReceiverTypes { get; set; } = new List<string>();

		IEnumerable<string> IChannelSchema.AllowedContentTypes => AllowedContentTypes;

		public IList<string> AllowedContentTypes { get; set; } = new List<string>();

		public IList<string> CredentialTypes { get; set; } = new List<string>();

		IEnumerable<string> IChannelSchema.CredentialTypes => CredentialTypes;

		public IList<string> Options { get; set; } = new List<string>();

		IEnumerable<string> IChannelSchema.Options => Options;
	}
}
