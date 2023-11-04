using System.Collections.ObjectModel;

namespace Deveel.Messaging {
	/// <summary>
	/// Extends the <see cref="IMessage"/> contract to provide
	/// methods to manipulate the message.
	/// </summary>
	public static class MessageExtensions {
		/// <summary>
		/// Indicates if the direction of the message is outbound.
		/// </summary>
		/// <param name="message">
		/// The message to check the direction.
		/// </param>
		/// <returns>
		/// Returns <c>true</c> if the message is outbound,
		/// otherwise <c>false</c>.
		/// </returns>
		/// <seealso cref="MessageDirection"/>
        public static bool IsOutbound(this IMessage message)
            => message.Direction == MessageDirection.Outbound;

		/// <summary>
		/// Indicates if the direction of the message is inbound.
		/// </summary>
		/// <param name="message">
		/// The message to check the direction.
		/// </param>
		/// <returns>
		/// Returns <c>true</c> if the message is inbound,
		/// otherwise <c>false</c>.
		/// </returns>
		/// <seealso cref="MessageDirection"/>
		public static bool IsInbound(this IMessage message)
            => message.Direction == MessageDirection.Inbound;

		/// <summary>
		/// Indicates if the given message is intended to be
		/// a test message.
		/// </summary>
		/// <param name="message">
		/// The message to check.
		/// </param>
		/// <returns>
		/// Returns <c>true</c> if the message is configured
		/// to be a test message, otherwise <c>false</c>.
		/// </returns>
		/// <seealso cref="KnownMessageOptions.Test"/>
		public static bool? IsTest(this IMessage message)
			=> message.Options?.TryGetValue(KnownMessageOptions.Test, out bool? isTest) ?? false ? isTest : null;

		public static bool HasRetry(this IMessage message)
			=> message.Options?.ContainsKey(KnownMessageOptions.Retry) ?? false;

		public static bool? Retry(this IMessage message)
			=> (message.Options?.TryGetValue(KnownMessageOptions.Retry, out bool? isRetry) ?? false) ? isRetry : null;

		public static int? RetryCount(this IMessage message)
			=> message.Options?.TryGetValue(KnownMessageOptions.RetryCount, out int? count) ?? false ? count : null;

		public static int? RetryDelay(this IMessage message)
			=> message.Options?.TryGetValue(KnownMessageOptions.RetryDelay, out int? delay) ?? false ? delay : null;

		public static int? Timeout(this IMessage message)
			=> message.Options?.TryGetValue(KnownMessageOptions.Timeout, out int? timeout) ?? false ? timeout : null;

		public static TimeSpan? Expiration(this IMessage message)
			=> message.Options?.TryGetValue(KnownMessageOptions.Expiration, out TimeSpan? expiration) ?? false ? expiration : null;

		/// <summary>
		/// Extends the message with a set of options.
		/// </summary>
		/// <param name="message">
		/// The instance of the message to extend.
		/// </param>
		/// <param name="options">
		/// The set of options to add to the message.
		/// </param>
		/// <returns>
		/// Returns a new instance of the message that
		/// has the given options merged with the existing.
		/// </returns>
		public static IMessage WithOptions(this IMessage message, IDictionary<string, object> options)
			=> new MessageWithOption(message, options);

		/// <summary>
		/// Extends the message with a single option.
		/// </summary>
		/// <param name="message">
		/// The instance of the message to extend.
		/// </param>
		/// <param name="optionName">
		/// The name of the option to add to the message.
		/// </param>
		/// <param name="value">
		/// The value of the option to add to the message.
		/// </param>
		/// <returns>
		/// Returns a new instance of the message that
		/// has the given option merged with the existing
		/// set of options.
		/// </returns>
		public static IMessage WithOption(this IMessage message, string optionName, object value)
			=> new MessageWithOption(message, new Dictionary<string, object> {
				{optionName, value}
			});

		public static IMessage WithRetry(this IMessage message, bool retry = true)
			=> message.With(KnownMessageOptions.Retry, retry);

		public static IMessage WithRetryCount(this IMessage message, int count)
			=> message.With(KnownMessageOptions.RetryCount, count);

		public static IMessage WithRetryDelay(this IMessage message, int delayMillis)
			=> message.With(KnownMessageOptions.RetryDelay, delayMillis);

		public static IMessage WithContext(this IMessage message, IDictionary<string, object> context)
			=> new MessageWithContext(message, new MessageContextProviderImpl(context), false);

		public static IMessage WithContext(this IMessage message, IMessageContextProvider contextProvider)
			=> new MessageWithContext(message, contextProvider);

		public static IMessage WithAttempt(this IMessage message, int attemptCount)
			=> message.With(KnownMessageProperties.Attempt, attemptCount);

		public static int? Attempt(this IMessage message)
			=> message.Properties?.TryGetValue(KnownMessageProperties.Attempt, out int? attemptCount) ?? false ? attemptCount : null;

		/// <summary>
		/// Extends the message with a set of properties.
		/// </summary>
		/// <param name="message">
		/// The instance of the message to extend.
		/// </param>
		/// <param name="properties">
		/// The set of properties to be merged.
		/// </param>
		/// <returns>
		/// Returns a new instance of the message that
		/// has the given properties merged with the existing.
		/// </returns>
		public static IMessage With(this IMessage message, IDictionary<string, object> properties)
			=> new MessageWithProperty(message, properties);

		public static IMessage With(this IMessage message, string propertyName, object value)
			=> new MessageWithProperty(message, new Dictionary<string, object> {
				{propertyName, value}
			});

		public static string? RemoteMessageId(this IMessage message)
			=> message.Properties?.TryGetValue(KnownMessageProperties.RemoteMessageId, out string? remoteId) ?? false ? remoteId : null;

		public static IMessage WithRemoteMessageId(this IMessage message, string remoteId)
			=> message.With(KnownMessageProperties.RemoteMessageId, remoteId);

		public static string? Subject(this IMessage message)
			=> message.Properties?.TryGetValue(KnownMessageProperties.Subject, out string? subject) ?? false ? subject : null;

		public static IMessage WithSubject(this IMessage message, string subject)
			=> message.With(KnownMessageProperties.Subject, subject);

		public static IMessage WithFallbackTo(this IMessage message, string messageId)
			=> message.With(KnownMessageProperties.FallbackMessageId, messageId);

		public static IMessage WithSender(this IMessage message, ITerminal sender)
			=> new MessageWithSender(message, sender);

        public static IMessage WithReceiver(this IMessage message, ITerminal receiver)
            => new MessageWithReceiver(message, receiver);

		public static IMessage WithChannel(this IMessage message, IMessageChannel channel)
			=> new MessageWithChannel(message, channel);

        public static IMessage WithContent(this IMessage message, IMessageContent content)
            => new MessageWithContent(message, content);

        public static IMessage AsReadOnly(this IMessage message)
            => new MessageWrapper(message);

		#region MessageContextProviderImpl

		class MessageContextProviderImpl : IMessageContextProvider {
			public IDictionary<string, object> Context { get; }

			public MessageContextProviderImpl(IDictionary<string, object> context) {
				Context = context;
			}
		}

		#endregion

		#region MessageWithOption

		class MessageWithOption : MessageWrapper {
			private readonly IDictionary<string, object> options;

			public MessageWithOption(IMessage message, IDictionary<string, object> options) : base(message) {
				if (message.Options == null) {
					this.options = new ReadOnlyDictionary<string, object>(options);
				} else {
					this.options = new ReadOnlyDictionary<string, object>(message.Options.Merge(options));
				}
			}

			public override IDictionary<string, object> Options => options;
		}

		#endregion

		#region MessageWithProperty

		class MessageWithProperty : MessageWrapper {
			private readonly IDictionary<string, object> properties;

			public MessageWithProperty(IMessage message, IDictionary<string, object> properties) : base(message) {
				if (message.Properties == null) {
					this.properties = new ReadOnlyDictionary<string, object>(properties);
				} else {
					this.properties = new ReadOnlyDictionary<string, object>(message.Properties.Merge(properties));
				}
			}

			public override IDictionary<string, object> Properties => properties;
		}

		#endregion

		#region MessageWithSender

		class MessageWithSender : MessageWrapper {
			private readonly ITerminal sender;

			public MessageWithSender(IMessage message, ITerminal sender)
				: base(message) {
				this.sender = sender;
			}

			public override ITerminal Sender => sender;
		}

        #endregion

        #region MessageWithReceiver

        class MessageWithReceiver : MessageWrapper {
            private readonly ITerminal receiver;

            public MessageWithReceiver(IMessage message, ITerminal receiver) : base(message) {
                this.receiver = receiver;
            }

            public override ITerminal Receiver => receiver;
        }

        #endregion

        #region MessageWithContext

        class MessageWithContext : MessageWrapper {
			private readonly IDictionary<string, object> context;

			public MessageWithContext(IMessage message, IMessageContextProvider contextProvider, bool mergeContext = true)
				: base(message) {
				if (message.Context == null) {
					context = new ReadOnlyDictionary<string, object>(contextProvider.Context);
				} else if (mergeContext) {
					context = new ReadOnlyDictionary<string, object>(contextProvider.Context.Merge(message.Context));
				} else {
					context = new ReadOnlyDictionary<string, object>(message.Context.Merge(contextProvider.Context));
				}
			}

			public override IDictionary<string, object> Context => context;
		}

		#endregion

		#region MessageWithChannel

		class MessageWithChannel : MessageWrapper {
			private readonly IMessageChannel channel;

			public MessageWithChannel(IMessage message, IMessageChannel channel)
				: base(message) {
				this.channel = channel;
			}

			public override IMessageChannel Channel => channel;
		}

        #endregion

        #region MessageWithContent

        class MessageWithContent : MessageWrapper {
            private readonly IMessageContent content;

            public MessageWithContent(IMessage message, IMessageContent content) : base(message) {
                this.content = content;
            }

            public override IMessageContent Content => content;
        }

        #endregion
	}
}
