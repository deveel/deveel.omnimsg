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

using System.Runtime.Serialization;

namespace Deveel.Messaging.Channels {
	/// <summary>
	/// The routing directions supported 
	/// by a channel.
	/// </summary>
	[Flags]
	public enum ChannelDirection {
		/// <summary>
		/// The channel doesn't support any routing.
		/// </summary>
		[EnumMember(Value = "none")]
		None = 0,

		/// <summary>
		/// The channel supports inbound routing
		/// of messages from queues.
		/// </summary>
		Inbound = 1,

		/// <summary>
		/// The channel supports outbound routing
		/// of messages to queues.
		/// </summary>
		Outbound = 2,

		/// <summary>
		/// The channel supports both inbound and
		/// outbound routing.
		/// </summary>
		Duplex = Inbound | Outbound
	}
}
