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
			Assert.True(schema.SupportsDuplex());
			Assert.True(schema.SupportsInbound());
			Assert.True(schema.SupportsOutbound());
			Assert.False(schema.RequiresReceivers());
			Assert.False(schema.RequiresSenders());
			Assert.False(schema.RequiresSendersOfType(KnownTerminalTypes.Email));
			Assert.False(schema.RequiresReceiversOfType(KnownTerminalTypes.Email));
			Assert.True(schema.AllowsTextContent());
			Assert.True(schema.AllowsHtmlContent());
			Assert.True(schema.AllowsMultipartContent());
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
			Assert.True(schema.SupportsDuplex());
			Assert.True(schema.SupportsInbound());
			Assert.True(schema.SupportsOutbound());
			Assert.False(schema.RequiresReceivers());
			Assert.False(schema.RequiresSenders());
			Assert.False(schema.RequiresReceivers());
			Assert.False(schema.RequiresSendersOfType(KnownTerminalTypes.Email));
			Assert.False(schema.RequiresReceiversOfType(KnownTerminalTypes.Email));
			Assert.True(schema.AllowsTextContent());
			Assert.True(schema.AllowsHtmlContent());
			Assert.False(schema.AllowsMultipartContent());
		}

		[Fact]
		public static void BuildChannelSchemaFromSource() {
			var schema = new ChannelSchema {
				Type = "test",
				Provider = "deveel",
				Directions = ChannelDirection.Duplex,
			};

			var builtSchema = new ChannelSchemaBuilder(schema)
				.WithAllowedContentTypes("*")
				.WithAllowedEmailSender()
				.Build();

			Assert.NotNull(builtSchema);
			Assert.Equal(schema.Type, builtSchema.Type);
			Assert.Equal(schema.Provider, builtSchema.Provider);
			Assert.Equal(schema.Directions, builtSchema.Directions);
			Assert.True(builtSchema.AllowsSendersOfType(KnownTerminalTypes.Email));
			Assert.True(builtSchema.AllowsContentType(KnownMessageContentTypes.Text));
			Assert.True(builtSchema.AllowsContentType(KnownMessageContentTypes.Html));
		}
    }
}
