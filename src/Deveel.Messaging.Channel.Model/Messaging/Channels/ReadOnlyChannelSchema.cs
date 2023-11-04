namespace Deveel.Messaging.Channels {
    class ReadOnlyChannelSchema : IChannelSchema {
        private readonly IChannelSchema schema;

        public ReadOnlyChannelSchema(IChannelSchema schema) {
            this.schema = schema;
        }

        public string Type => schema.Type;

        public string Provider => schema.Provider;

        public ChannelDirection Directions => schema.Directions;

        public IEnumerable<string> AllowedSenderTypes => schema.AllowedSenderTypes;

        public IEnumerable<string> RequiredSenderTypes => schema.RequiredSenderTypes;

        public IEnumerable<string> AllowedReceiverTypes => schema.AllowedReceiverTypes;

        public IEnumerable<string> RequiredReceiverTypes => schema.RequiredReceiverTypes;

        public IEnumerable<string> AllowedContentTypes => schema.AllowedContentTypes;

        public IEnumerable<string> CredentialTypes => schema.CredentialTypes;

        public IEnumerable<string> Options => schema.Options;
    }
}
