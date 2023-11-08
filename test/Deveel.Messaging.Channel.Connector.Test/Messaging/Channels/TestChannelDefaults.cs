using Deveel.Messaging.Channels;

namespace Deveel.Messaging.Channels {
	public static class TestChannelDefaults {
		public const string Type = "test";
		public const string Provider = "deveel";

		public const string SourceFormat = "object";

        public static readonly IChannelSchema Schema = new ChannelSchemaBuilder()
            .OfType(Type)
            .ByProvider(Provider)
            .WithDuplex()
            .WithAnyAllowedSenderType()
            .WithAnyAllowedReceiverType()
            .WithAnyAllowedContentType()
            .Build();
	}
}
