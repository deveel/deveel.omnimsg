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
	/// A terminal is a node in a network that is able 
	/// to send or receive messages
	/// </summary>
	/// <remarks>
	/// <para>
	/// A terminal is specialized to a specific protocol
	/// for sending and receiving messages: this can be
	/// an HTTP endpoint (eg. a URL), an e-mail address,
	/// a TCP/IP address, etc.
	/// </para>
	/// <para>
	/// A type of terminal can be used by more than one
	/// protocol.
	/// </para>
	/// </remarks>
	public interface ITerminal {
		/// <summary>
		/// Gets the type of the terminal that is used.
		/// </summary>
		string Type { get; }

		/// <summary>
		/// Gets the address of the terminal.
		/// </summary>
		string Address { get; }
	}
}