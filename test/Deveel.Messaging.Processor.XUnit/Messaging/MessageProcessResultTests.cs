namespace Deveel.Messaging {
	public static class MessageProcessResultTests {
		[Fact]
		public static void Success() {
			var message = new Message();
			var result = MessageProcessResult.Success(message);

			Assert.Equal(message, result.Message);
			Assert.True(result.Processed);
			Assert.True(result.Successful);
			Assert.Null(result.ErrorCode);
			Assert.Null(result.ErrorMessage);
		}

		[Fact]
		public static void Failed() {
			var message = new Message();
			var result = MessageProcessResult.Failed(message, "ERR-01", "Error message");

			Assert.Equal(message, result.Message);
			Assert.True(result.Processed);
			Assert.False(result.Successful);
			Assert.Equal("ERR-01", result.ErrorCode);
			Assert.Equal("Error message", result.ErrorMessage);
		}

		[Fact]
		public static void Unprocessed() {
			var message = new Message();
			var result = MessageProcessResult.Unprocessed(message);

			Assert.Equal(message, result.Message);
			Assert.False(result.Processed);
			Assert.False(result.Successful);
			Assert.Null(result.ErrorCode);
			Assert.Null(result.ErrorMessage);
		}
	}
}
