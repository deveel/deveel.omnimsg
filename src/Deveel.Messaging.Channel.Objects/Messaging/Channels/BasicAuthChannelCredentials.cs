namespace Deveel.Messaging.Channels {
	public sealed class BasicAuthChannelCredentials : ChannelCredentials, IBasicAuthChannelCredentials {
		public BasicAuthChannelCredentials(string username, string password) {
			Username = username;
			Password = password;
		}

		public BasicAuthChannelCredentials() {
		}

		protected override string CredentialsType => KnownChannelCredentialsTypes.BasicAuth;

		public string Username { get; set; }

		public string Password { get; set; }
	}
}
