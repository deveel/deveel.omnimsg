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
	/// An object that provides API Key credentials to a channel.
	/// </summary>
	public sealed class ApiKeyChannelCredentials : ChannelCredentials, IApiKeyChannelCredentials {
		/// <summary>
		/// Constructs the credentials object.
		/// </summary>
		public ApiKeyChannelCredentials() {
		}

		/// <summary>
		/// Constructs the credentials object with the given API key.
		/// </summary>
		/// <param name="apiKey"></param>
		public ApiKeyChannelCredentials(string apiKey) {
			ApiKey = apiKey;
		}

		/// <summary>
		/// Constructs the credentials object from another API Key.
		/// </summary>
		/// <param name="apiKey"></param>
		public ApiKeyChannelCredentials(IApiKeyChannelCredentials apiKey) {
			ApiKey = apiKey.ApiKey;
		}

		/// <inheritdoc/>
		public string ApiKey { get; set; }

		/// <inheritdoc/>
		protected override string CredentialsType => KnownChannelCredentialsTypes.ApiKey;
	}
}
