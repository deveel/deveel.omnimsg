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
	/// A message is a data structure that is sent from a
	/// sender to a receiver.
	/// </summary>
	public interface IMessage {
		/// <summary>
		/// Gets the unique identifier of the message
		/// within the network.
		/// </summary>
		string Id { get; }

		/// <summary>
		/// Gets the identifier of the tenant that is
		/// owning the message.
		/// </summary>
		string? TenantId { get; }

		/// <summary>
		/// Gets the reference to the channel that is used
		/// to transport the message.
		/// </summary>
		IMessageChannel Channel { get; }

		/// <summary>
		/// Gets the direction of the message in the scope
		/// of a queue.
		/// </summary>
		MessageDirection Direction { get; }

		/// <summary>
		/// Gets the terminal that is the sender of the message.
		/// </summary>
		ITerminal Sender { get; }

		/// <summary>
		/// Gets the terminal that is the receiver of the message.
		/// </summary>
		ITerminal Receiver { get; }

		/// <summary>
		/// Gets the content of the message.
		/// </summary>
		IMessageContent Content { get; }

		/// <summary>
		/// Gets a set of properties that extend the message.
		/// </summary>
		IDictionary<string, object>? Properties { get; }

		/// <summary>
		/// Gets a context that is attached to the message.
		/// </summary>
		IDictionary<string, object>? Context { get; }

		/// <summary>
		/// Gets a set of options that instruct the network
		/// of the behavior of transportation of the message.
		/// </summary>
		IDictionary<string, object>? Options { get; }

		/// <summary>
		/// Gets the timestamp of the creation of the message.
		/// </summary>
		DateTimeOffset TimeStamp { get; }
	}
}
