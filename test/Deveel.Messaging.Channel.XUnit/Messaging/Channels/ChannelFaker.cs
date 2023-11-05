using Bogus;

namespace Deveel.Messaging.Channels {
	public class ChannelFaker : Faker<Channel> {
		public ChannelFaker() {
			RuleFor(x => x.Id, f => f.Random.Guid().ToString());
			RuleFor(x => x.TenantId, f => f.Random.String2(20).OrNull(f));
			RuleFor(x => x.Type, f => f.PickRandom(ChannelTypes));
			RuleFor(x => x.Provider, (f, c) => f.PickRandom(ChannelProviders[c.Type]));
			RuleFor(x => x.Name, f => f.Lorem.Word());
			RuleFor(x => x.ContentTypes, (f, c) => ChannelContentTypes[c.Type]);
			RuleFor(x => x.Status, f => f.Random.Enum<ChannelStatus>(ChannelStatus.Unknown));
			RuleFor(x => x.Terminals, (f, c) => {
				if (f.Random.Bool())
					return null;

				if (c.Type == "sms")
					return new[] { new ChannelTerminal(KnownTerminalTypes.Phone, f.Phone.PhoneNumber("+############")) };
				if (c.Type == "email")
					return new[] { new ChannelTerminal(KnownTerminalTypes.Email, f.Internet.Email()) };

				return null;
			});

			RuleFor(x => x.Context, f => {
				if (f.Random.Bool())
					return null;

				var keys = f.Random.Int(1, 5);
				var context = new Dictionary<string, object>();
				for (var i = 0; i < keys; i++) {
					var key = f.Random.String2(10);
					var value = f.Random.Bool() ? (object)f.Random.String2(20) : f.Random.Int(1, 100);
					context[key] = value;
				}

				return context;
			});

			RuleFor(x => x.Credentials, (f, c) => {
				if (c.Provider == "twilio" ||
					c.Provider == "nexmo" ||
					c.Provider == "plivo")
					return new ChannelCredentials[] { new BasicAuthChannelCredentials(f.Internet.UserName(), f.Internet.Password()) };
				if (c.Provider == "mailgun" ||
					c.Provider == "sendgrid" ||
					c.Provider == "postmark" ||
					c.Provider == "onesignal" ||
					c.Provider == "pushwoosh" ||
					c.Provider == "pusher")
					return new ChannelCredentials[] { new ApiKeyChannelCredentials(f.Random.Guid().ToString()) };
				if (c.Provider == "facebook")
					return new ChannelCredentials[] { new TokenChannelCredentials(f.Random.Guid().ToString()) };

				return null;
			});

			// TODO: make the properties and options
			// TODO: make the credentials...
		}

		public static readonly string[] ChannelTypes = new[] {
			"sms", "email", "push", "messenger"
		};

		public static readonly IDictionary<string, string[]> ChannelProviders = new Dictionary<string, string[]> {
			["sms"] = new[] { "twilio", "nexmo", "plivo" },
			["email"] = new[] { "mailgun", "sendgrid", "postmark" },
			["push"] = new[] { "onesignal", "pushwoosh", "pusher" },
			["messenger"] = new[] { "facebook" }
		};

		public static readonly IDictionary<string, string[]> ChannelContentTypes = new Dictionary<string, string[]> {
			["sms"] = new[] { KnownMessageContentTypes.Text },
			["email"] = new[] { KnownMessageContentTypes.Text, KnownMessageContentTypes.Html, KnownMessageContentTypes.Multipart },
			["push"] = new[] { KnownMessageContentTypes.Text },
			["messenger"] = new[] { KnownMessageContentTypes.Text }
		};
	}
}
