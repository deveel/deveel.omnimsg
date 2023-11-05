namespace Deveel.Messaging.Channels {
	public sealed class TokenChannelCredentials : ChannelCredentials, ITokenChannelCredentials {
		public TokenChannelCredentials(string? scheme, string token) {
			Scheme = scheme;
			Token = token;
		}

		public TokenChannelCredentials(string token)
			: this(null, token) {
		}

		public TokenChannelCredentials() {
		}

		public TokenChannelCredentials(ITokenChannelCredentials credentials) {
			Scheme = credentials.Scheme;
			Token = credentials.Token;
		}

		protected override string CredentialsType => KnownChannelCredentialsTypes.Token;

		public string? Scheme { get; }

		public string Token { get; }
	}
}
