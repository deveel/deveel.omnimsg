// Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable CS8618

namespace Deveel.Messaging.Channels {
    class ChannelSchema : IChannelSchema {
        public ChannelSchema() {
        }

        public ChannelSchema(IChannelSchema schema) {
            Type = schema.Type;
            Provider = schema.Provider;
            Directions = schema.Directions;
            AllowedSenderTypes = schema.AllowedSenderTypes?.ToArray() ?? Array.Empty<string>();
            RequiredSenderTypes = schema.RequiredSenderTypes?.ToArray() ?? Array.Empty<string>();
            AllowedReceiverTypes = schema.AllowedReceiverTypes?.ToArray() ?? Array.Empty<string>();
            RequiredReceiverTypes = schema.RequiredReceiverTypes?.ToArray() ?? Array.Empty<string>();
            AllowedContentTypes = schema.AllowedContentTypes?.ToArray() ?? Array.Empty<string>();
            CredentialTypes = schema.CredentialTypes?.ToArray() ?? Array.Empty<string>();
            Options = schema.Options?.ToArray() ?? Array.Empty<string>();
        }

        public string Type { get; set; }

        public string Provider { get; set; }

        public ChannelDirection Directions { get; set; }

        public IEnumerable<string> AllowedSenderTypes { get; set; }

        public IEnumerable<string> RequiredSenderTypes { get; set; }

        public IEnumerable<string> AllowedReceiverTypes { get; set; }

        public IEnumerable<string> RequiredReceiverTypes { get; set; }

        public IEnumerable<string> AllowedContentTypes { get; set; }

        public IEnumerable<string> CredentialTypes { get; set; }

        public IEnumerable<string> Options { get; set; }
    }
}
