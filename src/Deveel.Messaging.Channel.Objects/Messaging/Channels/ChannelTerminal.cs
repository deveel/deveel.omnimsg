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
	public sealed class ChannelTerminal : IChannelTerminal {
		public ChannelTerminal(string type, string address, string? id = null) {
			Type = type;
			Address = address;
			Id = id;
		}

		public ChannelTerminal() {
		}

		public ChannelTerminal(IChannelTerminal terminal) {
			Id = terminal.Id;
			Type = terminal.Type;
			Address = terminal.Address;
		}

		/// <inheritdoc/>
		public string? Id { get; set; }

		/// <inheritdoc/>
		public string Type { get; set; }

		/// <inheritdoc/>
		public string Address { get; set; }
	}
}
