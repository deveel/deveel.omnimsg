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
	/// The reference to the instance of the channel
	/// that is used to transport the message.
	/// </summary>
	public interface IMessageChannel {
		/// <summary>
		/// Gets the unique identifier of the channel
		/// in the scope of the network.
		/// </summary>
		string Id { get; }

		/// <summary>
		/// Gets the type of the channel.
		/// </summary>
		string Type { get; }

		/// <summary>
		/// Gets the identifier of the provider of the channel.
		/// </summary>
		string Provider { get; }

		/// <summary>
		/// Gets the descriptive name of the channel.
		/// </summary>
		string Name { get; }
	}
}
