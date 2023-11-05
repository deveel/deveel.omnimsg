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
	/// An object tha provides a fluent interface to 
	/// build a channel schema.
	/// </summary>
	public sealed class ChannelSchemaBuilder {
		private readonly ChannelSchema schema;

		/// <summary>
		/// Constructs the builder with an empty schema.
		/// </summary>
		public ChannelSchemaBuilder()
			: this(new ChannelSchema()) {
		}

		/// <summary>
		/// Constructs the builder with the given schema
		/// to initialize the builder.
		/// </summary>
		/// <param name="schema">
		/// The schema to initialize the builder.
		/// </param>
		public ChannelSchemaBuilder(IChannelSchema schema) {
			this.schema = new ChannelSchema(schema);
		}

		/// <summary>
		/// Sets ths type of the channel.
		/// </summary>
		/// <param name="type">
		/// The type of the channel.
		/// </param>
		/// <returns>
		/// Returns the instance of the builder.
		/// </returns>
		/// <exception cref="ArgumentException">
		/// Thrown when the given <paramref name="type"/> is <c>null</c> 
		/// or empty.
		/// </exception>
		public ChannelSchemaBuilder OfType(string type) {
#if NET7_0_OR_GREATER
			ArgumentNullException.ThrowIfNullOrEmpty(type, nameof(type));
#else
			if (String.IsNullOrWhiteSpace(type))
				throw new ArgumentException($"'{nameof(type)}' cannot be null or whitespace.", nameof(type));
#endif
			schema.Type = type;
			return this;
		}

		/// <summary>
		/// Sets the provider of messaging services
		/// for the channel.
		/// </summary>
		/// <param name="provider">
		/// The identifier of the provider of the
		/// messging channel.
		/// </param>
		/// <returns>
		/// Returns the instance of the builder.
		/// </returns>
		/// <exception cref="ArgumentException"></exception>
		public ChannelSchemaBuilder ByProvider(string provider) {
            if (String.IsNullOrWhiteSpace(provider))
                throw new ArgumentException($"'{nameof(provider)}' cannot be null or whitespace.", nameof(provider));

            schema.Provider = provider;
			return this;
		}

		/// <summary>
		/// Sets the supported routing directions by
		/// the channel.
		/// </summary>
		/// <param name="direction">
		/// The direction of the channel.
		/// </param>
		/// <returns>
		/// Returns the instance of the builder.
		/// </returns>
		/// <exception cref="ArgumentException">
		/// Thrown when the given <paramref name="direction"/> is <see cref="ChannelDirection.None"/>.
		/// </exception>
		public ChannelSchemaBuilder WithDirection(ChannelDirection direction) {
            if (direction == ChannelDirection.None)
                throw new ArgumentException($"'{nameof(direction)}' cannot be {nameof(ChannelDirection.None)}.", nameof(direction));

			schema.Directions = schema.Directions | direction;
			return this;
		}

		/// <summary>
		/// Sets that the channel supports inbound routing.
		/// </summary>
		/// <returns>
		/// Returns the instance of the builder.
		/// </returns>
        public ChannelSchemaBuilder WithInbound()
            => WithDirection(ChannelDirection.Inbound);

		/// <summary>
		/// Sets that the channel supports outbound routing.
		/// </summary>
		/// <returns>
		/// Returns the instance of the builder.
		/// </returns>
        public ChannelSchemaBuilder WithOutbound()
            => WithDirection(ChannelDirection.Outbound);

		/// <summary>
		/// Sets that the channel supports both inbound 
		/// and outbound routing.
		/// </summary>
		/// <returns>
		/// Returns the instance of the builder.
		/// </returns>
        public ChannelSchemaBuilder WithDuplex()
            => WithDirection(ChannelDirection.Duplex);

        private static string[] NormalizeTypes(IEnumerable<string>? existingTypes, string[] types) {
            if (types is null)
                throw new ArgumentNullException(nameof(types));
            if (types.Length == 0)
                throw new ArgumentException($"'{nameof(types)}' cannot be empty.", nameof(types));

            var uniqueTypes = types.Distinct().ToArray();
            return existingTypes?.Union(uniqueTypes).ToArray() ?? uniqueTypes;
        }

		public ChannelSchemaBuilder WithAllowedSenderTypes(params string[] types) {
            schema.AllowedSenderTypes = NormalizeTypes(schema.AllowedSenderTypes, types);

			return this;
		}

        public ChannelSchemaBuilder WithAnyAllowedSenderType()
            => WithAllowedSenderTypes(ChannelSchemaDefaults.Any);

        public ChannelSchemaBuilder WithAllowedPhoneSender()
            => WithAllowedSenderTypes(KnownTerminalTypes.Phone);

        public ChannelSchemaBuilder WithAllowedEmailSender()
            => WithAllowedSenderTypes(KnownTerminalTypes.Email);

		public ChannelSchemaBuilder WithRequiredSenderTypes(params string[] types) {
            schema.RequiredSenderTypes = NormalizeTypes(schema.RequiredSenderTypes, types);
			return this;
		}

        public ChannelSchemaBuilder WithRequiredPhoneSender()
            => WithRequiredSenderTypes(KnownTerminalTypes.Phone);

        public ChannelSchemaBuilder WithRequiredEmailSender()
            => WithRequiredSenderTypes(KnownTerminalTypes.Email);

		public ChannelSchemaBuilder WithAllowedReceiverTypes(params string[] types) {
            schema.AllowedReceiverTypes = NormalizeTypes(schema.AllowedReceiverTypes, types);
			return this;
		}

        public ChannelSchemaBuilder WithAnyAllowedReceiverType()
            => WithAllowedReceiverTypes(ChannelSchemaDefaults.Any);

        public ChannelSchemaBuilder WithAllowedPhoneReceiver()
            => WithAllowedReceiverTypes(KnownTerminalTypes.Phone);

        public ChannelSchemaBuilder WithAllowedEmailReceiver()
            => WithAllowedReceiverTypes(KnownTerminalTypes.Email);

		public ChannelSchemaBuilder WithRequiredReceiverTypes(params string[] types) {
            schema.RequiredReceiverTypes = NormalizeTypes(schema.RequiredReceiverTypes, types);
			return this;
		}

        public ChannelSchemaBuilder WithRequiredPhoneReceiver()
            => WithRequiredReceiverTypes(KnownTerminalTypes.Phone);

        public ChannelSchemaBuilder WithRequiredEmailReceiver()
            => WithRequiredReceiverTypes(KnownTerminalTypes.Email);

		public ChannelSchemaBuilder WithAllowedContentTypes(params string[] types) {
			schema.AllowedContentTypes = NormalizeTypes(schema.AllowedContentTypes, types);
			return this;
		}

        public ChannelSchemaBuilder WithAnyAllowedContentType()
            => WithAllowedContentTypes(ChannelSchemaDefaults.Any);

        public ChannelSchemaBuilder WithAllowedTextContent()
            => WithAllowedContentTypes(KnownMessageContentTypes.Text);

        public ChannelSchemaBuilder WithAllowedHtmlContent()
            => WithAllowedContentTypes(KnownMessageContentTypes.Html);

        public ChannelSchemaBuilder WithAllowedMultipartContent()
            => WithAllowedContentTypes(KnownMessageContentTypes.Multipart);

		public ChannelSchemaBuilder WithCredentialTypes(params string[] types) {
			schema.CredentialTypes = NormalizeTypes(schema.CredentialTypes, types);
			return this;
		}

        public ChannelSchemaBuilder WithApiKey()
            => WithCredentialTypes(KnownChannelCredentialsTypes.ApiKey);

        public ChannelSchemaBuilder WithBasicAuth()
            => WithCredentialTypes(KnownChannelCredentialsTypes.BasicAuth);

        public ChannelSchemaBuilder WithToken()
            => WithCredentialTypes(KnownChannelCredentialsTypes.Token);

		public ChannelSchemaBuilder WithOptions(params string[] options) {
			schema.Options = NormalizeTypes(schema.Options, options);
			return this;
		}

		/// <summary>
		/// Validates and builds the channel schema
		/// with the current configuration.
		/// </summary>
		/// <returns>
		/// Returns an instance of <see cref="IChannelSchema"/>
		/// that has been built from the current configuration.
		/// </returns>
		/// <exception cref="InvalidOperationException">
		/// Thrown when the current configuration is not valid.
		/// </exception>
		public IChannelSchema Build() {
			Validate();

            return schema.AsReadOnly();
		}

		private void Validate() {
			if (schema.AllowedReceiverTypes != null && schema.RequiredReceiverTypes != null) {
				var allowed = schema.AllowedReceiverTypes;
				var required = schema.RequiredReceiverTypes;

				if (allowed.Intersect(required).Any())
					throw new InvalidOperationException("The allowed and required receiver types cannot intersect.");
			}

			if (schema.AllowedSenderTypes != null && schema.RequiredSenderTypes != null) {
				var allowed = schema.AllowedSenderTypes;
				var required = schema.RequiredSenderTypes;

				if (allowed.Intersect(required).Any())
					throw new InvalidOperationException("The allowed and required sender types cannot intersect.");
			}

			if ((schema.AllowedReceiverTypes != null || schema.RequiredReceiverTypes != null) 
				&& !schema.Directions.HasFlag(ChannelDirection.Inbound))
				throw new InvalidOperationException("The channel schema does not allow inbound routing.");

			if ((schema.AllowedSenderTypes != null || schema.RequiredSenderTypes != null) &&
				!schema.Directions.HasFlag(ChannelDirection.Outbound))
				throw new InvalidOperationException("The channel schema does not allow outbound routing.");
		}
	}
}
