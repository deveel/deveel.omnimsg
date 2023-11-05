using System.Text;
using System.Text.Json;

namespace Deveel.Messaging {
	public static class MessageSerializationTests {
		[Fact]
		public static void SerializeEmailWithHtmlContent() {
			var message = new Message {
				Sender = Terminal.Email("test@foo.com"),
				Receiver = Terminal.Email("user@example.com"),
				Content = new HtmlContent("<html><body><h1>Hello World</h1></body></html>"),
				Direction = MessageDirection.Outbound,
				Channel = new MessageChannel("email", "sendgrid", "email1"),
				Properties = new Dictionary<string, object> {
					{ "subject", "Test Email" }
				},
				Context = new Dictionary<string, object> {
					{ "test", true }
				}
			};

			var json = JsonSerializer.Serialize(message);

			Assert.NotNull(json);
			Assert.NotEmpty(json);

			var deserialized = JsonSerializer.Deserialize<Message>(json);

			Assert.NotNull(deserialized);
			Assert.Equal(message.Id, deserialized.Id);
			Assert.Equal(message.TenantId, deserialized.TenantId);
			Assert.Equal(message.Direction, deserialized.Direction);
			Assert.NotNull(deserialized.Receiver);
			Assert.NotNull(deserialized.Sender);
			Assert.Equal(message.Sender.Type, deserialized.Sender.Type);
			Assert.Equal(message.Sender.Address, deserialized.Sender.Address);
			Assert.Equal(message.Receiver.Type, deserialized.Receiver.Type);
			Assert.Equal(message.Receiver.Address, deserialized.Receiver.Address);
			Assert.NotNull(deserialized.Channel);
			Assert.Null(deserialized.Options);
			Assert.NotNull(deserialized.Properties);
			Assert.NotNull(deserialized.Context);
			Assert.NotNull(deserialized.Content);

			Assert.IsType<HtmlContent>(deserialized.Content);

			var htmlContent = (HtmlContent) deserialized.Content;
			Assert.NotNull(htmlContent);
			Assert.Equal("<html><body><h1>Hello World</h1></body></html>", htmlContent.Html);
		}

		[Fact]
		public static void SerializeEmailWithTextContent() {
			var message = new Message {
				Sender = Terminal.Email("test@foo.com"),
				Receiver = Terminal.Email("user@example.com"),
				Content = new TextContent("Hello World"),
				Direction = MessageDirection.Outbound,
				Channel = new MessageChannel("email", "sendgrid", "email1"),
				Properties = new Dictionary<string, object> {
					{ "subject", "Test Email" }
				}
			};

			var json = JsonSerializer.Serialize(message);

			Assert.NotNull(json);
			Assert.NotEmpty(json);

			var deserialized = JsonSerializer.Deserialize<Message>(json);

			Assert.NotNull(deserialized);
			Assert.Equal(message.Id, deserialized.Id);
			Assert.Equal(message.TenantId, deserialized.TenantId);
			Assert.Equal(message.Direction, deserialized.Direction);
			Assert.NotNull(deserialized.Receiver);
			Assert.NotNull(deserialized.Sender);
			Assert.Equal(message.Sender.Type, deserialized.Sender.Type);
			Assert.Equal(message.Sender.Address, deserialized.Sender.Address);
			Assert.Equal(message.Receiver.Type, deserialized.Receiver.Type);
			Assert.Equal(message.Receiver.Address, deserialized.Receiver.Address);
			Assert.NotNull(deserialized.Channel);
			Assert.Null(deserialized.Options);
			Assert.NotNull(deserialized.Properties);
			Assert.Null(deserialized.Context);

			Assert.IsType<TextContent>(deserialized.Content);

			var textContent = (TextContent) deserialized.Content;
			Assert.NotNull(textContent);
			Assert.Equal("Hello World", textContent.Text);
		}
	}
}
