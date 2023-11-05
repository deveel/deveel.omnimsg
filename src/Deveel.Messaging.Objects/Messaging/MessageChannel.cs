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
	/// <summary>
	/// Provides references to a channel
	/// that can be used to send or receive messages.
	/// </summary>
	public class MessageChannel : IMessageChannel {
		/// <summary>
		/// Constructs the channel with the given properties.
		/// </summary>
		/// <param name="type">
		/// The type of the channel.
		/// </param>
		/// <param name="provider">
		/// The identifier of the provider that owns the channel.
		/// </param>
		/// <param name="name">
		/// The unique name of the channel within the scope of
		/// the tenant of the channel.
		/// </param>
		/// <param name="id">
		/// The unique identifier of the channel.
		/// </param>
		public MessageChannel(string type, string provider, string? name = null, string? id = null) {
			Type = type;
			Provider = provider;
			Name = name;
			Id = id;
		}

		/// <summary>
		/// Constructs the channel from the given instance.
		/// </summary>
		/// <param name="channel">
		/// An instance of <see cref="IMessageChannel"/> that is used
		/// to initialize the properties of this instance.
		/// </param>
		public MessageChannel(IMessageChannel channel) 
			: this(channel.Type, channel.Provider, channel.Name, channel.Id) {
		}

		/// <summary>
		/// Constructs the channel with no properties set.
		/// </summary>
		public MessageChannel() {
		}

		/// <inheritdoc/>
		public string Type { get; set; }

		/// <inheritdoc/>
		public string Provider { get; set; }

		/// <inheritdoc/>
		public string Name { get; set; }

		/// <inheritdoc/>
		public string Id { get; set; }
	}
}
