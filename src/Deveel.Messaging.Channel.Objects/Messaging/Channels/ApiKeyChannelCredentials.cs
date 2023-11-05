namespace Deveel.Messaging.Channels {
	public sealed class ApiKeyChannelCredentials : ChannelCredentials, IApiKeyChannelCredentials {
		public ApiKeyChannelCredentials() {
		}

		public ApiKeyChannelCredentials(string apiKey) {
			ApiKey = apiKey;
		}

		public ApiKeyChannelCredentials(IApiKeyChannelCredentials apiKey) {
			ApiKey = apiKey.ApiKey;
		}

		public string ApiKey { get; set; }

		protected override string CredentialsType => KnownChannelCredentialsTypes.ApiKey;
	}
}
