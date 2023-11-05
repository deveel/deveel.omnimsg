using Xunit;

namespace Deveel.Messaging.Channels {
    public static class ChannelSchemaTests {
        [Fact]
        public static void TestChannelSchemaBuilder() {
            var schema = new ChannelSchemaBuilder()
                .OfType("test")
                .ByProvider("deveel")
                .WithDuplex()
                .WithAnyAllowedSenderType()
                .WithAnyAllowedReceiverType()
                .WithAnyAllowedContentType()
                .Build();

            Assert.NotNull(schema);
            Assert.Equal("test", schema.Type);
            Assert.Equal("deveel", schema.Provider);
            Assert.Equal(ChannelDirection.Duplex, schema.Directions);
            Assert.True(schema.AllowsSendersOfType("any"));
            Assert.True(schema.AllowsReceiversOfType("any"));
            Assert.True(schema.AllowsContentType("any"));
        }

        [Fact]
        public static void TestChannelSchemaBuilderWithTypes() {
            var schema = new ChannelSchemaBuilder()
                .OfType("test")
                .ByProvider("deveel")
                .WithDuplex()
                .WithAllowedEmailSender()
                .WithAllowedPhoneSender()
                .WithAllowedEmailReceiver()
                .WithAllowedPhoneReceiver()
                .WithAllowedTextContent()
                .WithAllowedHtmlContent()
                .Build();

            Assert.NotNull(schema);
            Assert.Equal("test", schema.Type);
            Assert.Equal("deveel", schema.Provider);
            Assert.Equal(ChannelDirection.Duplex, schema.Directions);
            Assert.True(schema.AllowsSendersOfType(KnownTerminalTypes.Email));
            Assert.True(schema.AllowsSendersOfType(KnownTerminalTypes.Phone));
            Assert.False(schema.AllowsSendersOfType(KnownTerminalTypes.Url));
            Assert.True(schema.AllowsReceiversOfType(KnownTerminalTypes.Email));
            Assert.True(schema.AllowsReceiversOfType(KnownTerminalTypes.Phone));
            Assert.False(schema.AllowsReceiversOfType(KnownTerminalTypes.Url));
            Assert.True(schema.AllowsContentType(KnownMessageContentTypes.Text));
            Assert.True(schema.AllowsContentType(KnownMessageContentTypes.Html));
            Assert.False(schema.AllowsContentType(KnownMessageContentTypes.Multipart));
        }
    }
}
