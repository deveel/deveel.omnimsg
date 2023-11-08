using Bogus;

namespace Deveel.Messaging.Channels {
	public class ChannelFaker : Faker<Channel> {
		public ChannelFaker(string? tenantId = null) {
			RuleFor(x => x.Id, f => f.Random.Guid().ToString());
			RuleFor(x => x.TenantId, f => tenantId ?? f.Random.String2(20).OrNull(f));
			RuleFor(x => x.Type, f => f.PickRandom(ChannelTypes));
			RuleFor(x => x.Provider, (f, c) => f.PickRandom(ChannelProviders[c.Type]));
			RuleFor(x => x.Name, f => f.Random.String2(14));
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

			RuleFor(x => x.Options, f => {
				var keys = f.Random.ListItems(ChannelOptions, 3);
				var options = new Dictionary<string, object>();
				foreach (var key in keys) {
					if (key == KnownChannelOptions.Test ||
						key == KnownChannelOptions.Retry) {
						options[key] = f.Random.Bool();
					} else if (key == KnownChannelOptions.RetryCount) {
						options[key] = f.Random.Int(1, 5);
					} else if (key == KnownChannelOptions.Timeout) {
						options[key] = f.Random.Int(100, 500);
					} else if (key == KnownChannelOptions.RetryDelay) {
						options[key] = f.Random.Int(200, 1000);
					}
				}

				return options;
			});

			RuleFor(x => x.Credentials, (f,c) => {
				if (!ChannelCredentialsTypes.TryGetValue(c.Provider, out var credTypes) ||
				credTypes.Length == 0)
					return null;

				if (credTypes[0] == KnownChannelCredentialsTypes.BasicAuth)
					return new ChannelCredentials[] { new BasicAuthChannelCredentials(f.Internet.UserName(), f.Internet.Password()) };
				if (credTypes[0] == KnownChannelCredentialsTypes.ApiKey)
					return new ChannelCredentials[] { new ApiKeyChannelCredentials(f.Random.Guid().ToString()) };
				if (credTypes[0] == KnownChannelCredentialsTypes.Token)
					return new ChannelCredentials[] { new TokenChannelCredentials(f.Random.Guid().ToString()) };

				return null;
			});

			// TODO: make the properties
		}

		public static readonly IList<string> ChannelTypes = new List<string> {
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

		public static readonly IList<string> ChannelOptions = new List<string> {
			KnownChannelOptions.Retry,
			KnownChannelOptions.RetryDelay,
			KnownChannelOptions.RetryCount,
			KnownChannelOptions.Test,
		};

		public static IDictionary<string, string[]> ChannelCredentialsTypes = new Dictionary<string, string[]> {
			["twilio"] = new[] {KnownChannelCredentialsTypes.BasicAuth},
			["sendgrid"] = new[] {KnownChannelCredentialsTypes.ApiKey},
			["facebook"] = new[] { KnownChannelCredentialsTypes.Token},
			["mailgun"] = new[] {KnownChannelCredentialsTypes.ApiKey},
			["onesignal"] = new[] {KnownChannelCredentialsTypes.Token},
			["nexmo"] = new[] {KnownChannelCredentialsTypes.BasicAuth} 
		};
	}
}
