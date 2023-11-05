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
	/// An instance of a messaging channel that is used
	/// to transport messages through a network.
	/// </summary>
	/// <remarks>
	/// <para>
	/// A channel is a logical entity that is used to
	/// provide configurations and options to the
	/// connection to a provider of messaging services.
	/// </para>
	/// <para>
	/// It is possible to have multiple channels of the
	/// same type, from the same provider, but with
	/// different configurations and options.
	/// </para>
	/// </remarks>
	public interface IChannel : IMessageContextProvider {
		/// <summary>
		/// Gets the unique identifier of the channel
		/// instance in the scope of the network.
		/// </summary>
		string Id { get; }

		/// <summary>
		/// Gets the identifier of the tenant that
		/// owns the channel.
		/// </summary>
		string? TenantId { get; }

		/// <summary>
		/// Gets the type of the channel.
		/// </summary>
		string Type { get; }

		/// <summary>
		/// Gets the identifier of the provider of
		/// messaging services, to which the channel
		/// is connected.
		/// </summary>
		string Provider { get; }

		/// <summary>
		/// Gets the descriptive name of the channel.
		/// </summary>
		string Name { get; }

		/// <summary>
		/// Gets the routing directions supported
		/// by the channel.
		/// </summary>
		ChannelDirection Directions { get; }

		/// <summary>
		/// Gets a reference to the terminals that are
		/// bound to the channel, as stable senders or 
		/// receivers.
		/// </summary>
		IEnumerable<IChannelTerminal>? Terminals { get; }

		/// <summary>
		/// Gets a set of content types that are supported
		/// by this instance of channel.
		/// </summary>
		IEnumerable<string> ContentTypes { get; }

		/// <summary>
		/// Gets a set of options that are used to configure
		/// the behavior of the channel.
		/// </summary>
		IDictionary<string, object>? Options { get; }

		/// <summary>
		/// Gets a set of credentials that are used to
		/// authenticate the channel towards the network.
		/// </summary>
		IEnumerable<IChannelCredentials>? Credentials { get; }

		/// <summary>
		/// Gets the current status of the channel.
		/// </summary>
		ChannelStatus Status { get; }
	}
}
