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

using System.Text.Json.Serialization;

namespace Deveel.Messaging.Channels {
	/// <summary>
	/// An object that provides credentials to a channel.
	/// </summary>
	[JsonConverter(typeof(ChannelCredentialsJsonConverter))]
	public abstract class ChannelCredentials : IChannelCredentials {
		string IChannelCredentials.CredentialsType => CredentialsType;

		/// <summary>
		/// When overridden in a derived class, gets the type of the credentials.
		/// </summary>
		protected abstract string CredentialsType { get; }

		/// <summary>
		/// Creates a new instance of a credentials object from the given contract.
		/// </summary>
		/// <param name="credentials">
		/// The credentials contract to create the object from.
		/// </param>
		/// <returns>
		/// Returns an instance of a <see cref="ChannelCredentials"/> object
		/// that is created from the given contract.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// Thrown if the given <paramref name="credentials"/> is <c>null</c>.
		/// </exception>
		/// <exception cref="NotSupportedException">
		/// Thrown when the type of the credentials is not supported.
		/// </exception>
		public static ChannelCredentials From(IChannelCredentials credentials) {
			ArgumentNullException.ThrowIfNull(credentials, nameof(credentials));

			if (credentials.CredentialsType == KnownChannelCredentialsTypes.ApiKey &&
				credentials is ApiKeyChannelCredentials apiKey)
				return new ApiKeyChannelCredentials(apiKey.ApiKey);

			if (credentials.CredentialsType == KnownChannelCredentialsTypes.BasicAuth &&
				credentials is BasicAuthChannelCredentials basic)
				return new BasicAuthChannelCredentials(basic.Username, basic.Password);

			if (credentials.CredentialsType == KnownChannelCredentialsTypes.Token &&
				credentials is TokenChannelCredentials token)
				return new TokenChannelCredentials(token.Scheme, token.Token);

			throw new NotSupportedException($"The credentials type '{credentials.CredentialsType}' is not supported.");
		}
	}
}
