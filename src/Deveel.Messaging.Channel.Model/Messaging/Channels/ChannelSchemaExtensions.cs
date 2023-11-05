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

namespace Deveel.Messaging.Channels {
	/// <summary>
	/// Extends the <see cref="IChannelSchema"/> contract that
	/// provides some helper methods to check the schema.
	/// </summary>
	public static class ChannelSchemaExtensions {
		public static bool RequiresSenders(this IChannelSchema schema) {
			ArgumentNullException.ThrowIfNull(schema, nameof(schema));

			return schema.RequiredSenderTypes?.Any() ?? false;
		}

		public static bool RequiresReceivers(this IChannelSchema schema) {
			ArgumentNullException.ThrowIfNull(schema, nameof(schema));

			return schema.RequiredReceiverTypes?.Any() ?? false;
		}

		public static bool RequiresSendersOfType(this IChannelSchema schema, string senderType) {
			ArgumentNullException.ThrowIfNull(schema, nameof(schema));
#if NET7_0_OR_GREATER
			ArgumentNullException.ThrowIfNullOrEmpty(senderType, nameof(senderType));
#else
			if (String.IsNullOrWhiteSpace(senderType))
				throw new ArgumentException($"'{nameof(senderType)}' cannot be null or whitespace.", nameof(senderType));
#endif
			return schema.RequiredSenderTypes?.Contains(senderType) ?? false;
		}

		public static bool RequiresReceiversOfType(this IChannelSchema schema, string receiverType) {
			ArgumentNullException.ThrowIfNull(schema, nameof(schema));
#if NET7_0_OR_GREATER
			ArgumentNullException.ThrowIfNullOrEmpty(receiverType, nameof(receiverType));
#else
			if (String.IsNullOrWhiteSpace(receiverType))
				throw new ArgumentException($"'{nameof(receiverType)}' cannot be null or whitespace.", nameof(receiverType));
#endif
			return schema.RequiredReceiverTypes?.Contains(receiverType) ?? false;
		}

		public static bool AllowsSenders(this IChannelSchema schema) {
			ArgumentNullException.ThrowIfNull(schema, nameof(schema));

			return schema.AllowedSenderTypes?.Any() ?? false;
		}

		public static bool AllowsReceivers(this IChannelSchema schema) {
			ArgumentNullException.ThrowIfNull(schema, nameof(schema));

			return schema.AllowedReceiverTypes?.Any() ?? false;
		}

		public static bool AllowsSendersOfType(this IChannelSchema schema, string senderType) {
			ArgumentNullException.ThrowIfNull(schema, nameof(schema));
#if NET7_0_OR_GREATER
			ArgumentNullException.ThrowIfNullOrEmpty(senderType, nameof(senderType));
#else
			if (String.IsNullOrWhiteSpace(senderType))
				throw new ArgumentException($"'{nameof(senderType)}' cannot be null or whitespace.", nameof(senderType));
#endif

			return (schema.AllowedSenderTypes?.Contains(senderType) ?? false) || 
                (schema.AllowedSenderTypes?.Contains(ChannelSchemaDefaults.Any) ?? false);
		}

		public static bool AllowsReceiversOfType(this IChannelSchema schema, string receiverType) {
#if NET7_0_OR_GREATER
			ArgumentNullException.ThrowIfNullOrEmpty(receiverType, nameof(receiverType));
#else
			if (String.IsNullOrWhiteSpace(receiverType))
				throw new ArgumentException($"'{nameof(receiverType)}' cannot be null or whitespace.", nameof(receiverType));
#endif
			ArgumentNullException.ThrowIfNull(schema, nameof(schema));
			return (schema.AllowedReceiverTypes?.Contains(receiverType) ?? false) ||
                (schema.AllowedReceiverTypes?.Contains(ChannelSchemaDefaults.Any) ?? false);
		}

        public static bool SupportsDirection(this IChannelSchema schema, ChannelDirection routing)
            => schema.Directions.HasFlag(routing);

        public static bool SupportsDuplex(this IChannelSchema schema)
            => schema.SupportsDirection(ChannelDirection.Duplex);

        public static bool SupportsInbound(this IChannelSchema schema)
            => schema.SupportsDirection(ChannelDirection.Inbound);

        public static bool SupportsOutbound(this IChannelSchema schema)
            => schema.SupportsDirection(ChannelDirection.Outbound);

        public static bool AllowsContentType(this IChannelSchema schema, string contentType)
            => (schema.AllowedContentTypes?.Contains(contentType) ?? false) ||
            (schema.AllowedContentTypes?.Contains(ChannelSchemaDefaults.Any) ?? false);

        public static bool AllowsTextContent(this IChannelSchema schema)
            => schema.AllowsContentType(KnownMessageContentTypes.Text);

        public static bool AllowsHtmlContent(this IChannelSchema schema)
            => schema.AllowsContentType(KnownMessageContentTypes.Html);

        public static bool AllowsMultipartContent(this IChannelSchema schema)
            => schema.AllowsContentType(KnownMessageContentTypes.Multipart);

		public static bool SupportsOption(this IChannelSchema schema, string optionKey)
			=> schema.Options?.Any(x => x == optionKey) ?? false;

        public static IChannelSchema AsReadOnly(this IChannelSchema schema) {
			ArgumentNullException.ThrowIfNull(schema, nameof(schema));

            return new ReadOnlyChannelSchema(schema);
        }

		public static bool RequiresCredentials(this IChannelSchema schema)
			=> schema.CredentialTypes?.Any() ?? false;
	}
}
