using System.ComponentModel.DataAnnotations;

using Xunit;

namespace Deveel.Messaging {
	public static class MessageTests {
		[Fact]
		public static void WithContext() {
			var message = new Message {
				Context = new Dictionary<string, object> {
					{ "a", 1 },
					{ "b", "test" }
				}
			};

			var context = new Dictionary<string, object> {
				{"a", 2},
				{"b", "test"}
			};

			var messageWithContext = message.WithContext(context);

			Assert.NotNull(messageWithContext);
			Assert.NotNull(messageWithContext.Context);

			Assert.Equal(2, messageWithContext.Context["a"]);
			Assert.Equal("test", messageWithContext.Context["b"]);
		}

		[Fact]
		public static void WithContextByProvider() {
			var channel = new ContextProvider {
				Context = new Dictionary<string, object> {
					{ "a", 1 },
					{ "b", "test" }
				}
			};

			var message = new Message {
				Context = new Dictionary<string, object> {
					{ "a", 2 },
					{ "c", 23.2 },
					{ "d", "foo" }
				}
			};

			var messageWithContext = message.WithContext(channel);

			Assert.NotNull(messageWithContext);
			Assert.NotNull(messageWithContext.Context);

			Assert.Equal(2, messageWithContext.Context["a"]);
			Assert.Equal("test", messageWithContext.Context["b"]);
			Assert.Equal(23.2, messageWithContext.Context["c"]);
			Assert.Equal("foo", messageWithContext.Context["d"]);
		}

		[Fact]
		public static void WithProperties() {
			var message = new Message {
				Properties = new Dictionary<string, object> {
					{ "a", 1 },
					{ "b", "test" }
				}
			};

			var properties = new Dictionary<string, object> {
				{"a", 2},
				{"b", "test"}
			};

			var messageWithProperty = message.With(properties);

			Assert.NotNull(messageWithProperty);
			Assert.NotNull(messageWithProperty.Properties);

			Assert.Equal(2, messageWithProperty.Properties["a"]);
			Assert.Equal("test", messageWithProperty.Properties["b"]);
		}

		[Fact]
		public static void WithProperty() {
			var message = new Message {
				Properties = new Dictionary<string, object> {
					{ "a", 1 },
					{ "b", "test" }
				}
			};

			var messageWithProperty = message.With("a", 2);

			Assert.NotNull(messageWithProperty);
			Assert.NotNull(messageWithProperty.Properties);

			Assert.Equal(2, messageWithProperty.Properties["a"]);
			Assert.Equal("test", messageWithProperty.Properties["b"]);
		}

		[Fact]
		public static void WithSender() {
			var message = new Message {
				Channel = new MessageChannel("test", "test")
			};

			var sender = new Terminal("test", "test");

			var messageWithSender = message.WithSender(sender);

			Assert.NotNull(messageWithSender);
			Assert.NotNull(messageWithSender.Sender);
			Assert.Equal(sender.Type, messageWithSender.Sender.Type);
			Assert.Equal(sender.Address, messageWithSender.Sender.Address);
		}

		[Fact]
		public static void WithReceiver() {
			var message = new Message {
				Channel = new MessageChannel("test", "test")
			};

			var receiver = new Terminal("test", "test");

			var messageWithReceiver = message.WithReceiver(receiver);

			Assert.NotNull(messageWithReceiver);
			Assert.NotNull(messageWithReceiver.Receiver);

			Assert.Equal(receiver.Type, messageWithReceiver.Receiver.Type);
			Assert.Equal(receiver.Address, messageWithReceiver.Receiver.Address);
		}

		[Fact]
		public static void GetIfTestMessage() {
			var message = new Message {
				Options = new Dictionary<string, object> {
					{ KnownMessageOptions.Test, true }
				}
			};

			var isTest = message.IsTest();

			Assert.NotNull(isTest);
			Assert.True(isTest.HasValue);
			Assert.True(isTest!.Value);
		}

		[Fact]
		public static void GetIfTestMessageWhenNotSet() {
			var message = new Message {
				Options = new Dictionary<string, object>()
			};

			var isTest = message.IsTest();

			Assert.Null(isTest);
		}

		[Fact]
		public static void GetIfTestMessageFromString() {
			var message = new Message {
				Options = new Dictionary<string, object> {
					{ KnownMessageOptions.Test, "true" }
				}
			};

			var isTest = message.IsTest();

			Assert.NotNull(isTest);
			Assert.True(isTest.HasValue);
			Assert.True(isTest!.Value);
		}

		[Fact]
		public static void GetIfRetry() {
			var message = new Message {
				Options = new Dictionary<string, object> {
					{ KnownMessageOptions.Retry, true }
				}
			};

			var isRetry = message.Retry();

			Assert.True(isRetry);
		}

		[Fact]
		public static void GetIfRetryWhenNotSet() {
			var message = new Message {
				Options = new Dictionary<string, object>()
			};

			var isRetry = message.Retry();

			Assert.Null(isRetry);
		}

		[Fact]
		public static void SetRetryOption() {
			var message = new Message {
				Options = new Dictionary<string, object>()
			};

			var result = message.WithRetry(true);

			Assert.NotNull(result.Options);
			Assert.True(result.Options.ContainsKey(KnownMessageOptions.Retry));
			Assert.True(result.Options[KnownMessageOptions.Retry] is bool);
			Assert.True((bool)result.Options[KnownMessageOptions.Retry]);
		}

		[Fact]
		public static void SetRetryCountOption() {
			var message = new Message {
				Options = new Dictionary<string, object>()
			};

			var result = message.WithRetryCount(5);

			Assert.NotNull(result.Options);
			Assert.True(result.Options.ContainsKey(KnownMessageOptions.RetryCount));
			Assert.True(result.Options[KnownMessageOptions.RetryCount] is int);
			Assert.Equal(5, (int)result.Options[KnownMessageOptions.RetryCount]);
		}

		[Theory]
		[InlineData(MessageDirection.Inbound, false)]
		[InlineData(MessageDirection.Outbound, true)]
		public static void IsOutbound(MessageDirection direction, bool expected) {
			var message = new Message {
				Direction = direction
			};

			var isOutbound = message.IsOutbound();

			Assert.Equal(expected, isOutbound);
		}

		[Theory]
		[InlineData(MessageDirection.Inbound, true)]
		[InlineData(MessageDirection.Outbound, false)]
		public static void IsInbound(MessageDirection direction, bool expected) {
			var message = new Message {
				Direction = direction
			};

			var isOutbound = message.IsInbound();

			Assert.Equal(expected, isOutbound);
		}

		[Fact]
		public static void WrappedMessage() {
			var message = new MessageFaker().Generate();
			//var message = new Message {
			//	Channel = new MessageChannel("test", "test"),
			//	Sender = new Terminal("test", "test"),
			//	Receiver = new Terminal("test", "test"),
			//	Direction = MessageDirection.Inbound,
			//	Content = new TextContent("test"),
			//	Options = new Dictionary<string, object> {
			//		{ KnownMessageOptions.Test, true }
			//	},
			//	Properties = new Dictionary<string, object> {
			//		{ "a", 1 },
			//		{ "b", "test" }
			//	},
			//	Context = new Dictionary<string, object> {
			//		{ "a", 2 },
			//		{ "c", 23.2 },
			//		{ "d", "foo" }
			//	}
			//};

			var wrapped = message.With("a", 2);

			Assert.NotNull(wrapped);
			Assert.Equal(message.Id, wrapped.Id);
			Assert.Equal(message.TenantId, wrapped.TenantId);
			Assert.Equal(message.Direction, wrapped.Direction);
			Assert.NotNull(wrapped.Receiver);
			Assert.NotNull(wrapped.Sender);
			Assert.Equal(message.Sender.Type, wrapped.Sender.Type);
			Assert.Equal(message.Sender.Address, wrapped.Sender.Address);
			Assert.Equal(message.Receiver.Type, wrapped.Receiver.Type);
			Assert.Equal(message.Receiver.Address, wrapped.Receiver.Address);
			Assert.NotNull(wrapped.Channel);
			Assert.NotNull(wrapped.Content);
			Assert.Equal(((IMessageContent) message.Content).ContentType, wrapped.Content.ContentType);

			Assert.Equal(message.Channel.Type, wrapped.Channel.Type);
			Assert.Equal(message.Channel.Provider, wrapped.Channel.Provider);
			Assert.Equal(message.Options?.Count, wrapped.Options?.Count);
			Assert.Equal((message.Properties?.Count ?? 0) + 1, wrapped.Properties?.Count);
			Assert.Equal(message.Context?.Count, wrapped.Context?.Count);
		}

		[Fact]
		public static void MessageFromMessage() {
			var message = new MessageFaker().Generate();
			var other = new Message(message);

			Assert.NotNull(other);
			Assert.Equal(message.Id, other.Id);
			Assert.Equal(message.TenantId, other.TenantId);
			Assert.Equal(message.Direction, other.Direction);
			Assert.NotNull(other.Receiver);
			Assert.NotNull(other.Sender);
			Assert.Equal(message.Sender.Type, other.Sender.Type);
			Assert.Equal(message.Sender.Address, other.Sender.Address);
			Assert.Equal(message.Receiver.Type, other.Receiver.Type);
			Assert.Equal(message.Receiver.Address, other.Receiver.Address);
			Assert.NotNull(other.Channel);
			Assert.NotNull(other.Content);
			Assert.Equal(((IMessageContent) message.Content).ContentType, ((IMessageContent) other.Content).ContentType);

			Assert.Equal(message.Channel.Type, other.Channel.Type);
			Assert.Equal(message.Channel.Provider, other.Channel.Provider);
			Assert.Equal(message.Options?.Count, other.Options?.Count);
			Assert.Equal(message.Properties?.Count, other.Properties?.Count);
			Assert.Equal(message.Context?.Count, other.Context?.Count);
		}


		#region ContextProvider

		class ContextProvider : IMessageContextProvider {
			public IDictionary<string, object> Context { get; set; }
		}

		#endregion
	}
}