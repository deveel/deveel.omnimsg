using Bogus;

namespace Deveel.Messaging {
	public class MessageFaker : Faker<Message> {
		public MessageFaker() {
			RuleFor(x => x.Id, f => f.Random.Guid().ToString());
			RuleFor(x => x.TenantId, f => f.Random.String2(10).OrNull(f));
			RuleFor(x => x.Direction, f => f.PickRandom<MessageDirection>());
			RuleFor(x => x.Channel, f => {
				var type = f.PickRandom(ChannelTypes);
				var provider = f.PickRandom(ChannelProviders[type]);
				var name = f.Random.String2(10);
				var id = f.Random.String2(10).OrNull(f);
				return new MessageChannel(type, provider, name, id);
			});
			RuleFor(x => x.Sender, (f, m) => {
				return m.Channel.Type switch {
					"email" => Terminal.Email(f.Internet.Email()),
					"sms" => Terminal.Phone(f.Phone.PhoneNumber("+#############")),
					"push" => Terminal.Url(f.Internet.Url()),
					"whatsapp" =>  Terminal.Phone(f.Phone.PhoneNumber()),
					"telegram" => Terminal.Application(f.Random.String2(20)),
					"viber" => Terminal.Phone(f.Phone.PhoneNumber()),
					"messenger" => Terminal.Application(f.Random.String2(10)),
					_ => throw new NotSupportedException($"Channel type '{m.Channel.Type}' is not supported")
				};
			});
			RuleFor(x => x.Receiver, (f, m) => {
				return m.Channel.Type switch {
					"email" => Terminal.Email(f.Internet.Email()),
					"sms" => Terminal.Phone(f.Phone.PhoneNumber("+#############")),
					"push" => Terminal.Device(f.Random.Guid().ToString()),
					"whatsapp" =>  Terminal.Phone(f.Phone.PhoneNumber()),
					"telegram" => Terminal.User(f.Random.String2(20)),
					"viber" => Terminal.Phone(f.Phone.PhoneNumber()),
					"messenger" => Terminal.User(f.Random.String2(10)),
					_ => throw new NotSupportedException($"Channel type '{m.Channel.Type}' is not supported")
				};
			});

			RuleFor(x => x.Content, (f, m) => {
				return m.Channel.Type switch {
					"sms" => new TextContent(f.Random.String2(100)),
					"email" => new HtmlContent($"<html><body><p>{f.Lorem.Paragraph(1)}</p></body></html>"),
					"push" => new TextContent(f.Random.String2(100)),
					"whatsapp" => new TextContent(f.Random.String2(100)),
					"telegram" => new TextContent(f.Random.String2(100)),
					"viber" => new TextContent(f.Random.String2(100)),
					"messenger" => new TextContent(f.Random.String2(100)),
					_ => throw new NotSupportedException($"Channel type '{m.Channel.Type}' is not supported")
				};
			});

			RuleFor(x => x.Options, f => {
				if (f.Random.Bool())
					return null;

				var options = new Dictionary<string, object>();
				var optionKeys = f.Random.ListItems(OptionKeys, 3);

				foreach (var key in optionKeys) {
					if (key == KnownMessageOptions.Test) {
						options[key] = f.Random.Bool();
					} else if (key == KnownMessageOptions.Retry) {
						options[key] = f.Random.Bool();
					} else if (key == KnownMessageOptions.RetryCount) {
						options[key] = f.Random.Int(1, 10);
					} else if (key == KnownMessageOptions.RetryDelay) {
						options[key] = f.Random.Int(100, 1000);
					} else if (key == KnownMessageOptions.Timeout) {
						options[key] = f.Random.Int(1000, 10000);
					} else if (key == KnownMessageOptions.Expiration) {
						options[key] = f.Random.Int(10000, 100000);
					}
				}

				return options;
			});

			RuleFor(x => x.Context, f => {
				if (f.Random.Bool())
					return null;

				var context = new Dictionary<string, object>();
				var keyCount = f.Random.Int(1, 5);
				for (var i = 0; i < keyCount; i++) {
					var key = f.Random.String2(10);
					var value = f.Random.Bool() ? (object) f.Random.Int(1, 100) : f.Random.String2(20);
					context[key] = value;
				}

				return context;
			});
		}

		public static string[] OptionKeys = new[] {
			KnownMessageOptions.Test,
			KnownMessageOptions.Retry,
			KnownMessageOptions.RetryCount,
			KnownMessageOptions.RetryDelay,
			KnownMessageOptions.Timeout,
			KnownMessageOptions.Expiration
		};

		public static string[] ChannelTypes = new[] {
			"email", "sms", "push", "whatsapp", "telegram", "viber", "messenger"
		};

		public static IDictionary<string, string[]> ChannelProviders = new Dictionary<string, string[]> {
			{ "email", new[] { "deveel", "sendgrid", "mailjet" } },
			{ "sms", new[] { "deveel", "twilio", "vonage", "linkmobility" } },
			{ "push", new[] { "deveel", "urbanairship", "firebase", "onesignal" } },
			{ "whatsapp", new[] { "deveel", "twilio", "linkmobility" } },
			{ "telegram", new[] { "telegram" } },
			{ "viber", new[] { "viber" } },
			{ "messenger", new[] { "facebook" } },
		};
	}
}
