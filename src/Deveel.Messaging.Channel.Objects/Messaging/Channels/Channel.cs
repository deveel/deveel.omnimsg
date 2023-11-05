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

namespace Deveel.Messaging.Channels {
	/// <summary>
	/// A default implementation of a messaging channel that is used
	/// to provide configurations and options to the connection to a
	/// provider of messaging services.
	/// </summary>
    public sealed class Channel : IChannel {
		/// <summary>
		/// Constructs the channel instance.
		/// </summary>
		public Channel() {
		}

		/// <summary>
		/// Constructs the channel instance from the given
		/// source channel.
		/// </summary>
		/// <param name="channel">
		/// The source channel to copy the properties from.
		/// </param>
		/// <exception cref="ArgumentNullException">
		/// Thrown if the given <paramref name="channel"/> is <c>null</c>.
		/// </exception>
		public Channel(IChannel channel) {
			ArgumentNullException.ThrowIfNull(channel, nameof(channel));

			Id = channel.Id;
			TenantId = channel.TenantId;
			Type = channel.Type;
			Provider = channel.Provider;
			Name = channel.Name;
			Directions = channel.Directions;
			Terminals = channel.Terminals?.Select(x => new ChannelTerminal(x)).ToList();
			ContentTypes = channel.ContentTypes?.ToArray() ?? Array.Empty<string>();
			Context = channel.Context?.ToDictionary(x => x.Key, y => y.Value);
			Options = channel.Options?.ToDictionary(x => x.Key, y => y.Value);
			Credentials = channel.Credentials?.Select(ChannelCredentials.From).ToList();
			Status = channel.Status;
		}

		/// <inheritdoc/>
		public string Type { get; set; }

		/// <inheritdoc/>
		public string Provider { get; set; }

		/// <inheritdoc/>
		public string Name { get; set; }

		/// <inheritdoc/>
		public ChannelDirection Directions { get; set; }

		IEnumerable<IChannelTerminal>? IChannel.Terminals => Terminals;

		public IList<ChannelTerminal>? Terminals { get; set; }

		IEnumerable<string> IChannel.ContentTypes => ContentTypes;

		public IList<string> ContentTypes { get; set;}

		/// <inheritdoc/>
		public IDictionary<string, object>? Context { get; set; }

		/// <inheritdoc/>
		public IDictionary<string, object>? Options { get; set; }

		IEnumerable<IChannelCredentials> IChannel.Credentials => Credentials;

		public IList<ChannelCredentials>? Credentials { get; set; }

		/// <inheritdoc/>
		public ChannelStatus Status { get; set; }

		/// <inheritdoc/>
		public string Id { get; set; }

		/// <inheritdoc/>
		public string? TenantId { get; set; }
	}
}
