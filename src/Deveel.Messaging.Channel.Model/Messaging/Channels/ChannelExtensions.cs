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
	/// Extends the base contract of a <see cref="IChannel"/> with
	/// further functionalities.
	/// </summary>
	public static class ChannelExtensions {
		/// <summary>
		/// Checks if the given channel is active.
		/// </summary>
		/// <param name="channel">
		/// The channel to check if active.
		/// </param>
		/// <returns>
		/// Returns <c>true</c> if the given channel is active, otherwise
		/// returns <c>false</c>.
		/// </returns>
        public static bool IsActive(this IChannel channel)
            => channel?.Status == ChannelStatus.Active;

		/// <summary>
		/// Checks if the given channel is able to send messages
		/// to a remote endpoint.
		/// </summary>
		/// <param name="channel">
		/// The channel to check if able to send messages.
		/// </param>
		/// <returns>
		/// Returns <c>true</c> if the given channel is able to send messages,
		/// otherwise returns <c>false</c>.
		/// </returns>
		public static bool CanSend(this IChannel channel)
			=> channel.Directions.HasFlag(ChannelDirection.Outbound);

		/// <summary>
		/// Checks if the given channel is able to receive messages
		/// from a remote endpoint.
		/// </summary>
		/// <param name="channel">
		/// The channel to check if able to receive messages.
		/// </param>
		/// <returns>
		/// Returns <c>true</c> if the given channel is able to receive messages,
		/// otherwise returns <c>false</c>.
		/// </returns>
		public static bool CanReceive(this IChannel channel)
			=> channel.Directions.HasFlag(ChannelDirection.Inbound);

		/// <summary>
		/// Gets a wrapper around the given channel that doesn't expose
		/// its credentials.
		/// </summary>
		/// <param name="channel">
		/// The channel instance to wrap.
		/// </param>
		/// <returns>
		/// Returns a <see cref="IChannel"/> instance that wraps the given
		/// channel and doesn't expose its credentials.
		/// </returns>
		public static IChannel WithoutCredentials(this IChannel channel)
			=> new ChannelWithoutCredentials(channel);

		/// <summary>
		/// Gets a wrapper around the given channel that includes
		/// the given credentials.
		/// </summary>
		/// <param name="channel">
		/// The channel instance to wrap.
		/// </param>
		/// <param name="credentials">
		/// The credentials used by the channel to access the 
		/// service provider.
		/// </param>
		/// <returns></returns>
		public static IChannel WithCredentials(this IChannel channel, IEnumerable<IChannelCredentials> credentials)
			=> new ChannelWithCredentials(channel, credentials);

		/// <summary>
		/// Checks if the given channel is a test channel.
		/// </summary>
		/// <param name="channel">
		/// The channel to check if is a test channel.
		/// </param>
		/// <remarks>
		/// Test channels are used to test the connectivity with a
		/// service provider, without actually sending or receiving 
		/// messages to/from a remote endpoint.
		/// </remarks>
		/// <returns>
		/// Returns <c>true</c> if the given channel is a test channel,
		/// otherwise returns <c>false</c>.
		/// </returns>
		/// <seealso cref="KnownChannelOptions.Test"/>
		/// <seealso cref="IChannel.Options"/>
		public static bool? IsTest(this IChannel channel)
			=> channel?.Options?.TryGetValue(KnownChannelOptions.Test, out bool? isTest) ?? false ? isTest : null;

		/// <summary>
		/// Checks if the given channel has a retry option.
		/// </summary>
		/// <param name="channel">
		/// The channel to check if has a retry option.
		/// </param>
		/// <remarks>
		/// This check is not indicative of the actual retry policy
		/// of the channel, but only if the channel has a retry option
		/// specified, either to enable or disable the retry policy.
		/// </remarks>
		/// <returns>
		/// Returns <c>true</c> if the given channel has a retry policy,
		/// otherwise returns <c>false</c>.
		/// </returns>
		/// <seealso cref="KnownChannelOptions.Retry"/>
		/// <seealso cref="IChannel.Options"/>
		public static bool HasRetry(this IChannel channel)
			=> channel?.Options?.ContainsKey(KnownChannelOptions.Retry) ?? false;

		public static bool? Retry(this IChannel channel)
			=> channel?.Options?.TryGetValue(KnownChannelOptions.Retry, out bool? isRetry) ?? false ? isRetry : null;

		public static int? RetryCount(this IChannel channel)
			=> channel?.Options?.TryGetValue(KnownChannelOptions.RetryCount, out int? count) ?? false ? count : null;

		public static int? Timeout(this IChannel channel)
			=> channel?.Options?.TryGetValue(KnownChannelOptions.Timeout, out int? timeout) ?? false ? timeout : null;

		public static TimeSpan? MessageExpiration(this IChannel channel)
			=> channel?.Options?.TryGetValue(KnownChannelOptions.MessageExpiration, out TimeSpan? expiration) ?? false ? expiration : null;

		/// <summary>
		/// Checks if the given channel has any credentials.
		/// </summary>
		/// <param name="channel">
		/// The channel to check if has credentials.
		/// </param>
		/// <returns>
		/// Returns <c>true</c> if the given channel has credentials,
		/// otherwise returns <c>false</c>.
		/// </returns>
		public static bool HasCredentials(this IChannel channel)
			=> channel.Credentials?.Any() ?? false;

		private static TCredentials? CredentialsOf<TCredentials>(this IChannel channel, string type)
			where TCredentials : class, IChannelCredentials
			=> channel.Credentials?.OfType<TCredentials>().FirstOrDefault(x => x.CredentialsType == type);

		/// <summary>
		/// Gets the API key credentials of the given channel,
		/// if any was specified.
		/// </summary>
		/// <param name="channel">
		/// The channel to get the API key credentials.
		/// </param>
		/// <returns>
		/// Returns an instance of <see cref="IApiKeyChannelCredentials"/> if
		/// the given channel has API key credentials, otherwise returns
		/// <c>null</c>.
		/// </returns>
		/// <seealso cref="IChannel.Credentials"/>
		/// <seealso cref="KnownChannelCredentialsTypes.ApiKey"/>
		public static IApiKeyChannelCredentials? ApiKey(this IChannel channel)
			=> channel.CredentialsOf<IApiKeyChannelCredentials>(KnownChannelCredentialsTypes.ApiKey);

		public static ITokenChannelCredentials? Token(this IChannel channel)
			=> channel.CredentialsOf<ITokenChannelCredentials>(KnownChannelCredentialsTypes.Token);

		public static IBasicAuthChannelCredentials? BasicAuth(this IChannel channel)
			=> channel.CredentialsOf<IBasicAuthChannelCredentials>(KnownChannelCredentialsTypes.BasicAuth);

		public static bool HasCredentialsOfType(this IChannel channel, string credentialsType)
			=> channel.Credentials?.Any(x => x.CredentialsType == credentialsType) ?? false;

		public static bool HasTerminals(this IChannel channel)
			=> channel?.Terminals?.Any() ?? false;

		public static bool HasTerminalsOfType(this IChannel channel, string terminalType)
			=> channel.Terminals?.Any(x => x.Type == terminalType) ?? false;

		#region ChannelWithCredentials

		class ChannelWithCredentials : ChannelWrapper {
			private readonly IChannel channel;
			private readonly IEnumerable<IChannelCredentials> credentials;

			public ChannelWithCredentials(IChannel channel, IEnumerable<IChannelCredentials> credentials) : base(channel) {
				this.channel = channel;
				this.credentials = credentials;
			}

			public override IEnumerable<IChannelCredentials> Credentials => credentials;
		}

		#endregion

		#region ChannelWithoutCredentials

		class ChannelWithoutCredentials : ChannelWrapper {
			private readonly IChannel channel;

			public ChannelWithoutCredentials(IChannel channel) : base(channel) {
				this.channel = channel;
			}

			public override IEnumerable<IChannelCredentials> Credentials => Enumerable.Empty<IChannelCredentials>();
		}

		#endregion
	}
}
