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

		private IMessage? LastLoggedMessage { get; set; }

		private IMessageState? LastState { get; set; }

		private IMessageState? LastLoggedState { get; set; }

		private int AttemptCount { get; set; }

		private void ConfigureServices(IServiceCollection services) {
			services.AddLogging(logging => logging.AddXUnit(outputHelper));

			if (!ChannelFaker.ChannelTypes.Contains(TestChannelDefaults.Type))
				ChannelFaker.ChannelProviders.Add(TestChannelDefaults.Type, new[] { "deveel" });
			if (!ChannelFaker.ChannelContentTypes.ContainsKey(TestChannelDefaults.Type))
				ChannelFaker.ChannelContentTypes.Add(TestChannelDefaults.Type, new[] { KnownMessageContentTypes.Text, KnownMessageContentTypes.Html });
			if (!ChannelFaker.ChannelTypes.Contains(TestChannelDefaults.Type))
				ChannelFaker.ChannelTypes.Add(TestChannelDefaults.Type);

			var testChannels = new ChannelFaker(TenantId).Generate(123);
			services.AddTestChannelResolver(testChannels);
			services.AddTestTerminalResolver(GetServerTerminals(testChannels));

			services.AddTestMessageLogger((message, token) => {
				outputHelper.WriteLine("Message: {0}", message.Id);
				LastLoggedMessage = message;
				return Task.CompletedTask;
			}, (state, token) => {
				outputHelper.WriteLine("State: {0}", state.MessageId);
				LastLoggedState = state;
				return Task.CompletedTask;
			});

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

		private IEnumerable<IServiceTerminal> GetServerTerminals(IList<Channel> testChannels) {
			var faker = new Faker<ServiceTerminal>()
				.RuleFor(x => x.Id, f => f.Random.Guid().ToString())
				.RuleFor(x => x.Name, f => f.Random.Word())
				.RuleFor(x => x.TenantId, TenantId)
				.RuleFor(x => x.Type, f => f.Random.ListItem(TerminalTypes))
				.RuleFor(x => x.Provider, (f, t) => RandomProvider(t))
				.RuleFor(x => x.Roles, f => f.Random.Enum<TerminalRole>(TerminalRole.None))
				.RuleFor(x => x.Address, (f, t) => f.Internet.Url())
				.RuleFor(x => x.Status, f => f.Random.Enum<TerminalStatus>(TerminalStatus.Unknown))
				.RuleFor(x => x.Channels, (f, t) => {
					var supported = testChannels.Where(c => Supports(c, t)).ToList();
					var count = f.Random.Int(0, supported.Count);
					var channels = f.Random.ListItems(supported, count);
					return channels.Select(c => new TerminalChannel(c.Id, c.Name)).ToList().OrNull(f);
				});
			return faker.Generate(23);
		}

		private string RandomProvider(ITerminal t)
			=> t.Type switch {
				KnownTerminalTypes.Email => "sendgrid",
				KnownTerminalTypes.Phone => "twilio",
				KnownTerminalTypes.Url => "deveel",
				_ => throw new NotSupportedException()
			};

		private bool Supports(IChannel channel, ITerminal terminal)
			=> channel.Type switch {
				KnownChannelTypes.Email => terminal.Type == KnownTerminalTypes.Email,
				KnownChannelTypes.Sms => terminal.Type == KnownTerminalTypes.Phone,
				KnownChannelTypes.WhatsApp => terminal.Type == KnownTerminalTypes.Phone,
				KnownChannelTypes.Web => terminal.Type == KnownTerminalTypes.Url,
				_ => false
			};

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
		}
	}
}
