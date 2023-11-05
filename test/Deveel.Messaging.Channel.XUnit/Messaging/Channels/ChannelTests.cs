namespace Deveel.Messaging.Channels {
	public static class ChannelTests {
		[Theory]
		[InlineData(ChannelDirection.Inbound, false)]
		[InlineData(ChannelDirection.Outbound, true)]
		public static void CanSend(ChannelDirection direction, bool expected) {
			var channel = new Channel {
				Name = "test",
				Directions = direction
			};

			Assert.Equal(expected, channel.CanSend());
		}

		[Theory]
		[InlineData(ChannelDirection.Inbound, true)]
		[InlineData(ChannelDirection.Outbound, false)]
		public static void CanReceive(ChannelDirection direction, bool expected) {
			var channel = new Channel {
				Name = "test",
				Directions = direction
			};

			Assert.Equal(expected, channel.CanReceive());
		}

		[Fact]
		public static void WithoutCredentials() {
			var channel = new Channel {
				Name = "test",
				Credentials = new ChannelCredentials[] {
					new ApiKeyChannelCredentials("test")
				}
			};

			var newChannel = channel.WithoutCredentials();

			Assert.Equal(channel.Name, newChannel.Name);
			Assert.NotNull(newChannel);
		}

		[Fact]
		public static void WithCredentials() {
			var channel = new Channel {
				Name = "test"
			};

			var newChannel = channel.WithCredentials(new[] {
				new ApiKeyChannelCredentials("test")
			});


			Assert.NotNull(newChannel);
			Assert.Equal(channel.Name, newChannel.Name);
			Assert.NotNull(newChannel.Credentials);
			Assert.NotEmpty(newChannel.Credentials);
			Assert.Single(newChannel.Credentials);
			
			var credentials = newChannel.Credentials.First();

			Assert.NotNull(credentials);
			Assert.Equal(KnownChannelCredentialsTypes.ApiKey, credentials.CredentialsType);

			var apiKey = Assert.IsAssignableFrom<IApiKeyChannelCredentials>(credentials);
			Assert.Equal("test", apiKey.ApiKey);
		}

		[Fact]
		public static void HasCredentials() {
			var channel = new Channel {
				Name = "test",
				Credentials = new ChannelCredentials[] {
					new ApiKeyChannelCredentials("test")
				}
			};

			Assert.True(channel.HasCredentials());
		}

		[Fact]
		public static void ApiKey() {
			var channel = new Channel {
				Name = "test",
				Credentials = new ChannelCredentials[] {
					new ApiKeyChannelCredentials("test")
				}
			};

			var apiKey = channel.ApiKey();
			Assert.NotNull(apiKey);
			Assert.Equal("test", apiKey.ApiKey);
		}

		[Fact]
		public static void BasicAuth() {
			var channel = new Channel {
				Name = "test",
				Credentials = new ChannelCredentials[] {
					new BasicAuthChannelCredentials("test", "test")
				}
			};

			var basicAuth = channel.BasicAuth();
			Assert.NotNull(basicAuth);
			Assert.Equal("test", basicAuth.Username);
			Assert.Equal("test", basicAuth.Password);
		}

		[Theory]
		[InlineData(true)]
		[InlineData(false)]
		public static void IsTest(bool isTest) {
			var channel = new Channel {
				Name = "test",
				Options = new Dictionary<string, object> {
					{KnownChannelOptions.Test, isTest}
				}
			};

			Assert.Equal(isTest, channel.IsTest());
		}

		[Theory]
		[InlineData(true)]
		[InlineData(false)]
		public static void HasRetry(bool retry) {
			var channel = new Channel {
				Name = "test",
				Options = new Dictionary<string, object> {
					{KnownChannelOptions.Retry, retry}
				}
			};

			Assert.True(channel.HasRetry());
			Assert.Equal(retry, channel.Retry());
		}

		[Theory]
		[InlineData(10)]
		[InlineData(20)]
		public static void RetryCount(int count) {
			var channel = new Channel {
				Name = "test",
				Options = new Dictionary<string, object> {
					{KnownChannelOptions.RetryCount, count}
				}
			};

			Assert.Equal(count, channel.RetryCount());
		}
	}
}
