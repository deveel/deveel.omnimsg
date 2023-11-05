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
	/// The direction of a message in a queue.
	/// </summary>
	/// <remarks>
	/// The direction of a message is relative to
	/// the application that is using the queue.
	/// </remarks>
	public enum MessageDirection {
		/// <summary>
		/// The message is outbound from the queue,
		/// and it is going to be sent to a remote
		/// provider or service.
		/// </summary>
		Outbound = 1,

		/// <summary>
		/// The message is inbound to the queue,
		/// and it is coming from a remote provider
		/// </summary>
		Inbound = 2
	}
}
