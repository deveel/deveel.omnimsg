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
	/// Describes a static token that is used to authenticate
	/// a connection to a messaging channel.
	/// </summary>
	public interface ITokenChannelCredentials : IChannelCredentials {
		/// <summary>
		/// Gets the authentication scheme used by the token.
		/// </summary>
		string? Scheme { get; }

		/// <summary>
		/// Gets the token used to authenticate the connection.
		/// </summary>
		string Token { get; }
	}
}
