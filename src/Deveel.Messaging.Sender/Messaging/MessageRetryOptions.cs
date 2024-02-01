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
	/// Configures the options for retrying the sending
	/// of a message.
	/// </summary>
	public sealed class MessageRetryOptions {
		/// <summary>
		/// Gets or sets the strategy to use to retry
		/// the sending of a message.
		/// </summary>
		public MessageRetryStrategy? Strategy { get; set; }

		/// <summary>
		/// Gets or sets the delay to wait before retrying
		/// the sending of a message.
		/// </summary>
		public TimeSpan? Delay { get; set; }

		/// <summary>
		/// Gets or sets the default number of times 
		/// to retry the sending of a message.
		/// </summary>
		public int? MaxRetries { get; set; }
	}
}
