using Bogus;

using Deveel.Messaging.Channels;
using Deveel.Messaging.Terminals;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Xunit.Abstractions;

namespace Deveel.Messaging {
	public class SenderTests {
		public SenderTests(ITestOutputHelper outputHelper) {
			this.outputHelper = outputHelper;

			var services = new ServiceCollection();
			ConfigureServices(services);

			Services = services.BuildServiceProvider().CreateScope().ServiceProvider;
		}

		public IServiceProvider Services { get; }

		public MessageSender Sender => Services.GetRequiredService<MessageSender>();

		public TestChannelResolver ChannelResolver => Services.GetRequiredService<TestChannelResolver>();

		public TestTerminalResolver TerminalResolver => Services.GetRequiredService<TestTerminalResolver>();

		public string TenantId => "test-tenant";

		private IMessage? LastSentMessage { get; set; }

		private IMessageState? LastState { get; set; }

		private int AttemptCount { get; set; }

		private void ConfigureServices(IServiceCollection services) {
			services.AddLogging(logging => logging.AddXUnit(outputHelper));

			if (!ChannelFaker.ChannelTypes.Contains(TestChannelDefaults.Type))
				ChannelFaker.ChannelProviders.Add(TestChannelDefaults.Type, new[] { "deveel" });
			if (!ChannelFaker.ChannelContentTypes.ContainsKey(TestChannelDefaults.Type))
				ChannelFaker.ChannelContentTypes.Add(TestChannelDefaults.Type, new[] { KnownMessageContentTypes.Text, KnownMessageContentTypes.Html });
			if (!ChannelFaker.ChannelTypes.Contains(TestChannelDefaults.Type))
				ChannelFaker.ChannelTypes.Add(TestChannelDefaults.Type);

			services.AddTestChannelResolver(new ChannelFaker(TenantId).Generate(123));
			services.AddTestTerminalResolver(GetServerTerminals());

			services.AddTestConnector(onSend: (message, token) => {
				AttemptCount++;
				LastSentMessage = message;

				var error = message.TestError();
				var result = error != null
					? MessageResult.Fail(error)
					: MessageResult.Success(message.WithRemoteMessageId(Guid.NewGuid().ToString()));

				return Task.FromResult(result);
			});


			services.AddSender(sender => sender
					.Configure(options => {
						options.Retry.MaxRetries = 3;
						options.StateHandler = (state) => {
							LastState = state;
							return Task.CompletedTask;
						};
					}));
		}

		private static readonly IList<string> TerminalTypes = new[] {
			KnownTerminalTypes.Email,
			KnownTerminalTypes.Phone,
			KnownTerminalTypes.Url
		};

		private readonly ITestOutputHelper outputHelper;

		private IEnumerable<IServiceTerminal> GetServerTerminals() {
			var faker = new Faker<ServiceTerminal>()
				.RuleFor(x => x.Id, f => f.Random.Guid().ToString())
				.RuleFor(x => x.TenantId, TenantId)
				.RuleFor(x => x.Type, f => f.Random.ListItem(TerminalTypes))
				.RuleFor(x => x.Address, (f, t) => f.Internet.Url())
				.RuleFor(x => x.Status, f => f.Random.Enum<TerminalStatus>(TerminalStatus.Unknown));

			return faker.Generate(23);
		}

		private IChannel RandomChannel(Func<IChannel, bool>? filter = null) {
			var i = 0;
			var channels = ChannelResolver.Channels;
			while (true) {
				var channel = channels[Random.Shared.Next(0, channels.Count - 1)];
				if (filter == null || filter(channel))
					return channel;

				if (i++ > 100)
					throw new InvalidOperationException("Unable to find a channel that matches the predicate");
			}
		}

		private IServiceTerminal RandomTerminal(Func<IServiceTerminal, bool>? filter = null) {
			var terminals = TerminalResolver.Terminals;
			while (true) {
				var terminal = terminals[Random.Shared.Next(0, terminals.Count - 1)];
				if (filter == null || filter(terminal))
					return terminal;
			}
		}

		[Fact]
		public async Task SendMessage_Success() {
			var testChannel = RandomChannel(c => c.Type == TestChannelDefaults.Type && c.IsActive());

			var message = new Message {
				Id = Guid.NewGuid().ToString(),
				TenantId = TenantId,
				Channel = new MessageChannel(TestChannelDefaults.Type, TestChannelDefaults.Provider, testChannel.Name),
				Sender = Terminal.Email("test@example.com"),
				Receiver = Terminal.Email("other@sample.com"),
				Content = new TextContent("Hello World!"),
				Properties = new Dictionary<string, object> {
					{ "email.subject", "Test Message" }
				}
			};

			var result = await Sender.SendAsync(message);

			Assert.True(result.Successful);
			Assert.Null(result.Error);

			Assert.NotNull(LastSentMessage);
			Assert.Equal(message.Id, LastSentMessage!.Id);
			Assert.Equal(message.TenantId, LastSentMessage!.TenantId);

			Assert.NotNull(LastState);
			Assert.Equal(MessageStatus.Sent, LastState.Status);
			// TODO: Assert.Equal(1, LastState!.RetryAttempt);
		}

		[Fact]
		public async Task SendMessage_NotSupportedChannel() {
			var testChannel = RandomChannel(c => c.Type != TestChannelDefaults.Type);

			var message = new Message {
				Id = Guid.NewGuid().ToString(),
				TenantId = TenantId,
				Channel = new MessageChannel(testChannel.Type, testChannel.Provider, testChannel.Name),
				Sender = Terminal.Email("test@example.com"),
				Receiver = Terminal.Email("other@sample.com"),
				Content = new TextContent("Hello World!"),
				Properties = new Dictionary<string, object> {
					{ "email.subject", "Test Message" }
				}
			};

			var result = await Sender.SendAsync(message);

			Assert.False(result.Successful);
			Assert.NotNull(result.Error);

			Assert.Equal(MessagingErrorCode.ChannelNotSupported, result.Error.Code);

			Assert.Null(LastSentMessage);

			Assert.Null(LastState);
		}

		[Fact]
		public async Task SendMessage_NotFoundChannel() {
			var message = new Message {
				Id = Guid.NewGuid().ToString(),
				TenantId = TenantId,
				Channel = new MessageChannel(TestChannelDefaults.Type, TestChannelDefaults.Provider, "not-found"),
				Sender = Terminal.Email("test@example.com"),
				Receiver = Terminal.Email("other@sample.com"),
				Content = new TextContent("Hello World!")
			};

			var result = await Sender.SendAsync(message);

			Assert.False(result.Successful);
			Assert.NotNull(result.Error);

			Assert.Equal(MessagingErrorCode.ChannelNotFound, result.Error.Code);

			Assert.Null(LastSentMessage);
			Assert.Null(LastState);
		}

		[Fact]
		public async Task SendMessage_ChannelNotActive() {
			var testChannel = RandomChannel(c => c.Type == TestChannelDefaults.Type && c.Status != ChannelStatus.Active);

			var message = new Message {
				Id = Guid.NewGuid().ToString(),
				TenantId = TenantId,
				Channel = new MessageChannel(TestChannelDefaults.Type, TestChannelDefaults.Provider, testChannel.Name),
				Sender = Terminal.Email("dev@example.com"),
				Receiver = Terminal.Email("user@foo.com")
			};

			var result = await Sender.SendAsync(message);

			Assert.False(result.Successful);
			Assert.NotNull(result.Error);
			Assert.Equal(MessagingErrorCode.ChannelNotActive, result.Error.Code);

			Assert.Null(LastSentMessage);
			Assert.Null(LastState);
		}

		[Fact]
		public async Task SendMessage_WithRetry() {
			var testChannel = RandomChannel(c => 
			c.Type == TestChannelDefaults.Type && c.IsActive() && (c.Retry() ?? false));

			IMessage message = new Message {
				Id = Guid.NewGuid().ToString(),
				TenantId = TenantId,
				Channel = new MessageChannel(TestChannelDefaults.Type, TestChannelDefaults.Provider, testChannel.Name),
				Sender = Terminal.Email("dev@example.com")
			};

			message = message
				.WithTestError(ChannelMessageError.Transient("00129", "Unable to send the message"))
				.WithRetryCount(3);

			var result = await Sender.SendAsync(message);

			Assert.False(result.Successful);
			Assert.NotNull(result.Error);

			var error = Assert.IsType<MessageError>(result.Error);

			Assert.Equal(MessagingErrorCode.ChannelNotAvailable, result.Error.Code);
			Assert.NotNull(result.Error.InnerError);

			var innerError = Assert.IsAssignableFrom<IChannelMessageError>(result.Error.InnerError);
			Assert.Equal("00129", innerError.Code);
			Assert.Equal(MessageErrorType.Transient, innerError.ErrorType);
			Assert.Equal(4, AttemptCount);

			Assert.NotNull(LastSentMessage);

			Assert.NotNull(LastState);
			Assert.Equal(MessageStatus.DeliveryFailed, LastState.Status);
			// TODO: Assert.Equal(4, LastState!.RetryAttempt);
		}
	}
}
