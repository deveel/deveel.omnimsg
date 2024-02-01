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
	/// The types of retry strategies that can be used 
	/// when sending a message.
	/// </summary>
	public enum MessageRetryStrategy {
		/// <summary>
		/// A retry strategy that uses an exponential backoff
		/// for the delay between each retry.
		/// </summary>
		ExponentialBackoff,

		/// <summary>
		/// A retry strategy that uses a linear backoff
		/// for the delay between each retry.
		/// </summary>
		LinearBackoff,

		/// <summary>
		/// A constant retry strategy that uses a fixed
		/// time delay between each retry.
		/// </summary>
		Constant
	}
}
