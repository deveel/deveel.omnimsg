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
	class ChannelWrapper : IChannel {
		private readonly IChannel channel;

		public ChannelWrapper(IChannel channel) {
			this.channel = channel;
		}

		public virtual string Type => channel.Type;

		public virtual string Provider => channel.Provider;

		public virtual string Name => channel.Name;

		public virtual ChannelDirection Directions => channel.Directions;

		public virtual IEnumerable<IChannelTerminal>? Terminals => channel.Terminals;

		public virtual IEnumerable<string> ContentTypes => channel.ContentTypes;

		public virtual IDictionary<string, object>? Options => channel.Options;

		public virtual IEnumerable<IChannelCredentials>? Credentials => channel.Credentials;

		public virtual ChannelStatus Status => channel.Status;

		public virtual string Id => channel.Id;

		public virtual string? TenantId => channel.TenantId;

		public virtual IDictionary<string, object> Context => channel.Context;
	}
}
