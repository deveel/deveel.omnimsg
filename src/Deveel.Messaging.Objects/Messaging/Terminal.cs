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
	/// A part of a messaging system that can be used to 
	/// send or receive messages.
	/// </summary>
	public class Terminal : ITerminal {
		/// <summary>
		/// Constructs the terminal with the given type and address.
		/// </summary>
		/// <param name="type">
		/// The type of the terminal.
		/// </param>
		/// <param name="address">
		/// The address of the terminal, specific to its type.
		/// </param>
		public Terminal(string type, string address) {
			Type = type;
			Address = address;
		}

		/// <summary>
		/// Constructs the terminal with no properties set.
		/// </summary>
		public Terminal() {
		}

		/// <summary>
		/// Constructs the terminal from the given instance.
		/// </summary>
		/// <param name="terminal">
		/// The source instance of <see cref="ITerminal"/> that is used
		/// to initialize the properties of this instance.
		/// </param>
		public Terminal(ITerminal terminal) 
			: this(terminal.Type, terminal.Address) {
		}

		/// <inheritdoc/>
		public string Type { get; set; }

		/// <inheritdoc/>
		public string Address { get; set; }

		/// <summary>
		/// Creates a new instance of <see cref="Terminal"/> with the given
		/// type and address.
		/// </summary>
		/// <param name="type">
		/// The type of the terminal to create
		/// </param>
		/// <param name="address">
		/// The address of the terminal, specific to its type.
		/// </param>
		/// <returns>
		/// Returns an instance of <see cref="Terminal"/> that represents
		/// the terminal with the given type and address.
		/// </returns>
		/// <exception cref="ArgumentException"></exception>
		public static Terminal Create(string type, string address) {
			if (string.IsNullOrWhiteSpace(type)) 
				throw new ArgumentException($"'{nameof(type)}' cannot be null or whitespace.", nameof(type));
			if (string.IsNullOrWhiteSpace(address))
				throw new ArgumentException($"'{nameof(address)}' cannot be null or whitespace.", nameof(address));

			return new Terminal(type, address);
		}

		/// <summary>
		/// Create a new terminal that represents an 
		/// email address.
		/// </summary>
		/// <param name="address">
		/// The address of the email.
		/// </param>
		/// <returns>
		/// Returns an instance of <see cref="Terminal"/> that represents
		/// an email address.
		/// </returns>
		public static Terminal Email(string address)
			=> Create(KnownTerminalTypes.Email, address);

		/// <summary>
		/// Creates a new terminal that represents a phone number.
		/// </summary>
		/// <param name="number">
		/// The phone number of the terminal.
		/// </param>
		/// <returns>
		/// Returns an instance of <see cref="Terminal"/> that represents
		/// a phone number.
		/// </returns>
		public static Terminal Phone(string number) 
			=> Create(KnownTerminalTypes.Phone, number);

		public static Terminal Url(string address) 
			=> Create(KnownTerminalTypes.Url, address);

		public static Terminal Application(string appId)
			=> Create(KnownTerminalTypes.Application, appId);

		public static Terminal User(string userId)
			=> Create(KnownTerminalTypes.UserId, userId);

		public static Terminal Device(string deviceId)
			=> Create(KnownTerminalTypes.DeviceId, deviceId);
	}
}
