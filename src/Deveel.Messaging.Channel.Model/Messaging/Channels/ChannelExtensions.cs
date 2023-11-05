namespace Deveel.Messaging.Channels {
	public static class ChannelExtensions {
        public static bool IsActive(this IChannel channel)
            => channel?.Status == ChannelStatus.Active;

		public static bool CanSend(this IChannel channel)
			=> channel.Directions.HasFlag(ChannelDirection.Outbound);

		public static bool CanReceive(this IChannel channel)
			=> channel.Directions.HasFlag(ChannelDirection.Inbound);

		public static IChannel WithoutCredentials(this IChannel channel)
			=> new ChannelWithoutCredentials(channel);

		public static IChannel WithCredentials(this IChannel channel, IEnumerable<IChannelCredentials> credentials)
			=> new ChannelWithCredentials(channel, credentials);

		public static bool? IsTest(this IChannel channel)
			=> channel?.Options?.TryGetValue(KnownChannelOptions.Test, out bool? isTest) ?? false ? isTest : null;

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

		public static bool HasCredentials(this IChannel channel)
			=> channel.Credentials?.Any() ?? false;

		private static TCredentials? CredentialsOf<TCredentials>(this IChannel channel, string type)
			where TCredentials : class, IChannelCredentials
			=> channel.Credentials?.OfType<TCredentials>().FirstOrDefault(x => x.CredentialsType == type);

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
