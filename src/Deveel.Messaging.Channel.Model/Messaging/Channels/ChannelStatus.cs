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
	/// <summary>
	/// The status of a channel in the network.
	/// </summary>
	public enum ChannelStatus {
		/// <summary>
		/// The status of the channel is unknown.
		/// </summary>
		Unknown = 0,

		/// <summary>
		/// The channel is active and can be used
		/// to send and receive messages.
		/// </summary>
		Active = 1,

		/// <summary>
		/// The channel is disabled and cannot be used
		/// to send or receive messages.
		/// </summary>
		Disabled = 2
	}
}
