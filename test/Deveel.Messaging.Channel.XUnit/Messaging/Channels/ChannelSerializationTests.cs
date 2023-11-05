using System.Text.Json;

namespace Deveel.Messaging.Channels {
	public static class ChannelSerializationTests {
		[Fact]
		public static void SerializeChannel() {
			var channel = new ChannelFaker().Generate();

			var json = JsonSerializer.Serialize(channel);

			var deserialized = JsonSerializer.Deserialize<Channel>(json);

			Assert.NotNull(deserialized);

			Assert.Equal(channel.Id, deserialized.Id);
			Assert.Equal(channel.TenantId, deserialized.TenantId);
			Assert.Equal(channel.Type, deserialized.Type);
			Assert.Equal(channel.Provider, deserialized.Provider);
			Assert.Equal(channel.Name, deserialized.Name);
			Assert.Equal(channel.ContentTypes, deserialized.ContentTypes);
			Assert.Equal(channel.Status, deserialized.Status);
		}
	}
}
