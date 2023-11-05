namespace Deveel.Messaging.Channels {
	public sealed class ChannelSchema : IChannelSchema {
		public ChannelSchema() {
		}

		public string Type { get; set; }

		public string Provider { get; set; }

		public ChannelDirection Directions { get; set; }

		IEnumerable<string> IChannelSchema.AllowedSenderTypes => AllowedSenderTypes;

		public IList<string> AllowedSenderTypes { get; set; } = new List<string>();

		IEnumerable<string> IChannelSchema.RequiredSenderTypes => RequiredSenderTypes;

		public IList<string> RequiredSenderTypes { get; set; } = new List<string>();

		IEnumerable<string> IChannelSchema.AllowedReceiverTypes => AllowedReceiverTypes;

		public IList<string> AllowedReceiverTypes { get; set; } = new List<string>();

		IEnumerable<string> IChannelSchema.RequiredReceiverTypes => RequiredReceiverTypes;

		public IList<string> RequiredReceiverTypes { get; set; } = new List<string>();

		IEnumerable<string> IChannelSchema.AllowedContentTypes => AllowedContentTypes;

		public IList<string> AllowedContentTypes { get; set; } = new List<string>();

		public IList<string> CredentialTypes { get; set; } = new List<string>();

		IEnumerable<string> IChannelSchema.CredentialTypes => CredentialTypes;

		public IList<string> Options { get; set; } = new List<string>();

		IEnumerable<string> IChannelSchema.Options => Options;
	}
}
