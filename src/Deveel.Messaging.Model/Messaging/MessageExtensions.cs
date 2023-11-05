// Copyright 2023 Deveel AS
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

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

		#region Options

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

		/// <summary>
		/// Gets a value indicating if the message has
		/// a retry option set.
		/// </summary>
		/// <param name="message">
		/// The instance of the message to check.
		/// </param>
		/// <returns>
		/// Returns <c>true</c> if the message has a retry
		/// option set, or <c>false</c> or <c>null</c> if not.
		/// </returns>
		public static bool? Retry(this IMessage message)
			=> (message.Options?.TryGetValue(KnownMessageOptions.Retry, out bool? isRetry) ?? false) ? isRetry : null;

		/// <summary>
		/// Gets the number of times the message should
		/// be retried to be delivered.
		/// </summary>
		/// <param name="message">
		/// The instance of the message to check.
		/// </param>
		/// <returns>
		/// Returns the number of times the message should
		/// be retried to be delivered, or <c>null</c> if
		/// the message has no retry option set.
		/// </returns>
		public static int? RetryCount(this IMessage message)
			=> message.Options?.TryGetValue(KnownMessageOptions.RetryCount, out int? count) ?? false ? count : null;

		/// <summary>
		/// Gets the delay in milliseconds that should be
		/// waited before retrying to deliver the message.
		/// </summary>
		/// <param name="message">
		/// The instance of the message to check.
		/// </param>
		/// <returns>
		/// Returns the delay in milliseconds that should be
		/// waited before retrying to deliver the message,
		/// or <c>null</c> if the message has no retry option set.
		/// </returns>
		public static int? RetryDelay(this IMessage message)
			=> message.Options?.TryGetValue(KnownMessageOptions.RetryDelay, out int? delay) ?? false ? delay : null;

		/// <summary>
		/// Gets the timeout in milliseconds that should be
		/// waited before the message retry is considered
		/// timed-out.
		/// </summary>
		/// <param name="message">
		/// The instance of the message to check.
		/// </param>
		/// <returns>
		/// Returns a value indicating the timeout in milliseconds
		/// that should be waited before the message retry is
		/// considered timed-out, or <c>null</c> if the message
		/// has no timeout option set.
		/// </returns>
		public static int? Timeout(this IMessage message)
			=> message.Options?.TryGetValue(KnownMessageOptions.Timeout, out int? timeout) ?? false ? timeout : null;

		/// <summary>
		/// Gets a value indicating the expiration of 
		/// the message.
		/// </summary>
		/// <param name="message">
		/// The instance of the message to check.
		/// </param>
		/// <returns>
		/// Returns a value indicating the expiration of
		/// the message, or <c>null</c> if the message
		/// has no expiration option set.
		/// </returns>
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

		/// <summary>
		/// Extends the message with a retry option.
		/// </summary>
		/// <param name="message">
		/// The instance of the message to extend.
		/// </param>
		/// <param name="retry">
		/// The boolean value indicating if the message
		/// should be retried to be delivered, or not.
		/// </param>
		/// <returns>
		/// Returns a new instance of the message that
		/// has the retry option set.
		/// </returns>
		public static IMessage WithRetry(this IMessage message, bool retry = true)
			=> message.WithOption(KnownMessageOptions.Retry, retry);

		/// <summary>
		/// Sets the number of times the message should
		/// be retried to be delivered.
		/// </summary>
		/// <param name="message">
		/// The instance of the message to extend.
		/// </param>
		/// <param name="count">
		/// The number of times the message should be
		/// retried to be delivered.
		/// </param>
		/// <returns>
		/// Returns a new instance of the message that
		/// has the retry count option set.
		/// </returns>
		public static IMessage WithRetryCount(this IMessage message, int count)
			=> message.WithOption(KnownMessageOptions.RetryCount, count);

		/// <summary>
		/// Sets the delay in milliseconds that should be
		/// waited before retrying to deliver the message.
		/// </summary>
		/// <param name="message">
		/// The instance of the message to extend.
		/// </param>
		/// <param name="delayMillis">
		/// The delay in milliseconds that should be waited
		/// before retrying to deliver the message.
		/// </param>
		/// <returns>
		/// Returns a new instance of the message that
		/// has the retry delay option set.
		/// </returns>
		public static IMessage WithRetryDelay(this IMessage message, int delayMillis)
			=> message.WithOption(KnownMessageOptions.RetryDelay, delayMillis);

		#endregion

		public static IMessage WithContext(this IMessage message, IDictionary<string, object> context)
			=> new MessageWithContext(message, new MessageContextProviderImpl(context), false);

		public static IMessage WithContext(this IMessage message, IMessageContextProvider contextProvider)
			=> new MessageWithContext(message, contextProvider);

		#region Properties

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

		/// <summary>
		/// Extends the message with a single property.
		/// </summary>
		/// <param name="message">
		/// The instance of the message to extend.
		/// </param>
		/// <param name="propertyName">
		/// The name of the property to add to the message.
		/// </param>
		/// <param name="value">
		/// The value of the property to add to the message.
		/// </param>
		/// <returns>
		/// Returns a new instance of the message that
		/// has the given property merged with the existing
		/// set of properties.
		/// </returns>
		public static IMessage With(this IMessage message, string propertyName, object value)
			=> new MessageWithProperty(message, new Dictionary<string, object> {
				{propertyName, value}
			});

		/// <summary>
		/// Sets the delivery attempt number of the message.
		/// </summary>
		/// <param name="message">
		/// The instance of the message to extend.
		/// </param>
		/// <param name="attemptCount">
		/// A value indicating the number of times the message.
		/// </param>
		/// <returns>
		/// Returns a new instance of the message that
		/// has the delivery attempt property set.
		/// </returns>
		public static IMessage WithAttempt(this IMessage message, int attemptCount)
			=> message.With(KnownMessageProperties.Attempt, attemptCount);

		public static int? Attempt(this IMessage message)
			=> message.Properties?.TryGetValue(KnownMessageProperties.Attempt, out int? attemptCount) ?? false ? attemptCount : null;

		public static string? RemoteMessageId(this IMessage message)
			=> message.Properties?.TryGetValue(KnownMessageProperties.RemoteMessageId, out string? remoteId) ?? false ? remoteId : null;

		public static IMessage WithRemoteMessageId(this IMessage message, string remoteId)
			=> message.With(KnownMessageProperties.RemoteMessageId, remoteId);

		/// <summary>
		/// Gets the subject property of the message.
		/// </summary>
		/// <param name="message">
		/// The instance of the message that has the 
		/// subject property.
		/// </param>
		/// <returns></returns>
		public static string? Subject(this IMessage message)
			=> message.Properties?.TryGetValue(KnownMessageProperties.Subject, out string? subject) ?? false ? subject : null;

		/// <summary>
		/// Extends the message with a subject property.
		/// </summary>
		/// <param name="message">
		/// The instance of the message to extend.
		/// </param>
		/// <param name="subject">
		/// The subject of the message.
		/// </param>
		/// <returns>
		/// Returns a new instance of the message that
		/// has the subject property set.
		/// </returns>
		public static IMessage WithSubject(this IMessage message, string subject)
			=> message.With(KnownMessageProperties.Subject, subject);

		#endregion

		/// <summary>
		/// Sets the sender of the message.
		/// </summary>
		/// <param name="message">
		/// The instance of the message to extend.
		/// </param>
		/// <param name="sender">
		/// The terminal that is the sender of the message.
		/// </param>
		/// <returns>
		/// Returns a new instance of the message that
		/// has the sender set.
		/// </returns>
		public static IMessage WithSender(this IMessage message, ITerminal sender)
			=> new MessageWithSender(message, sender);

		/// <summary>
		/// Sets the receiver of the message.
		/// </summary>
		/// <param name="message">
		/// The instance of the message to extend.
		/// </param>
		/// <param name="receiver">
		/// The terminal that is the receiver of the message.
		/// </param>
		/// <returns>
		/// Returns a new instance of the message that
		/// has the receiver set.
		/// </returns>
        public static IMessage WithReceiver(this IMessage message, ITerminal receiver)
            => new MessageWithReceiver(message, receiver);

		public static IMessage WithChannel(this IMessage message, IMessageChannel channel)
			=> new MessageWithChannel(message, channel);

        public static IMessage WithContent(this IMessage message, IMessageContent content)
            => new MessageWithContent(message, content);

		/// <summary>
		/// Makes the given message read-only.
		/// </summary>
		/// <param name="message">
		/// The instance of the message to make read-only.
		/// </param>
		/// <returns>
		/// Returns a new instance of the message that
		/// is read-only.
		/// </returns>
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
