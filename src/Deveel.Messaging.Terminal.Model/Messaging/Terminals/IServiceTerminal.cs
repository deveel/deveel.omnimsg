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

namespace Deveel.Messaging.Terminals {
	/// <summary>
	/// A terminal that is registered by a service and
	/// that can be used to send or receive messages.
	/// </summary>
	/// <remarks>
	/// <para>
	/// While a simple <see cref="ITerminal"/> is a volatile
	/// reference used as simple endpoint between parties for 
	/// sending or receiving messages, a <see cref="IServiceTerminal"/>
	/// is stable instance of a terminal that is registered by
	/// an organization.
	/// </para>
	/// <para>
	/// The <see cref="IServiceTerminal"/> provides
	/// a secure way to determine if the terminal is
	/// owned by a trusted party and if the terminal
	/// is active and available for communication.
	/// </para>
	/// <para>
	/// These endpoints are generally provided by the
	/// same provider of the messaging service that
	/// provides a channel, since they represent a
	/// unique entity that is registered by the service
	/// provider.
	/// </para>
	/// </remarks>
	public interface IServiceTerminal : ITerminal, IMessageContextProvider {
		/// <summary>
		/// Gets the unique identifier of the terminal.
		/// </summary>
		string Id { get; }

		/// <summary>
		/// Gets the identifier of the tenant that owns
		/// the terminal.
		/// </summary>
		string? TenantId { get; }

		/// <summary>
		/// Gets the name of the terminal.
		/// </summary>
		string Name { get; }

		/// <summary>
		/// Gets the identifier of the provider that
		/// provided the terminal.
		/// </summary>
		string Provider { get; }

		/// <summary>
		/// Gets the roles that the terminal has.
		/// </summary>
		TerminalRole Roles { get; }

		/// <summary>
		/// Gets the current status of the terminal.
		/// </summary>
		TerminalStatus Status { get; }

		/// <summary>
		/// Gets a list of channels that are bound to
		/// this terminal.
		/// </summary>
		IEnumerable<ITerminalChannel>? Channels { get; }
	}
}
