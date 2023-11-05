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
	class MessageWrapper : IMessage {
		private readonly IMessage message;

		public MessageWrapper(IMessage message) {
			this.message = message;
		}

		public virtual string Id => message.Id;

		public virtual string? TenantId => message.TenantId;

		public virtual ITerminal Sender => message.Sender;

		public virtual ITerminal Receiver => message.Receiver;

		public virtual IMessageChannel Channel => message.Channel;

		public virtual IMessageContent Content => message.Content;

		public virtual MessageDirection Direction => message.Direction;

		public virtual IDictionary<string, object>? Options => message.Options;

		public virtual IDictionary<string, object>? Properties => message.Properties;

		public virtual IDictionary<string, object>? Context => message.Context;

		public virtual DateTimeOffset TimeStamp => message.TimeStamp;
	}
}
