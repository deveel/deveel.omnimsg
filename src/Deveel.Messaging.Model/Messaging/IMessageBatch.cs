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
	/// A batch of messages that are exchanged between two terminals.
	/// </summary>
	public interface IMessageBatch : IMessageContextProvider {
		/// <summary>
		/// Gets the unique identifier of the batch.
		/// </summary>
		string Id { get; }

		/// <summary>
		/// Gets a set of options that are used to configure the
		/// behaviors of the channel when handling the messages
		/// included in the batch.
		/// </summary>
		/// <remarks>
		/// Batch-level options can be used to override the default
		/// options defined at the channel level, but they can also
		/// be overridden by the message-level options.
		/// </remarks>
		/// <seealso cref="IMessage.Options"/>
		IDictionary<string, object>? Options { get; }

		/// <summary>
		/// Gets the messages included in the batch.
		/// </summary>
		IEnumerable<IMessage> Messages { get; }
	}
}
