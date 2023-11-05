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
	/// A pair of username and password used to authenticate
	/// the connection to a channel.
	/// </summary>
	public interface IBasicAuthChannelCredentials : IChannelCredentials {
		/// <summary>
		/// Gets the username used to authenticate the connection.
		/// </summary>
		string Username { get; }

		/// <summary>
		/// Gets the secret password that is used to authenticate
		/// the connection.
		/// </summary>
		string Password { get; }
	}
}
