namespace Deveel.Messaging.Channels {
	public sealed class ChannelSchemaBuilder {
		private readonly ChannelSchema schema;

		public ChannelSchemaBuilder()
			: this(new ChannelSchema()) {
		}

		public ChannelSchemaBuilder(IChannelSchema schema) {
			this.schema = new ChannelSchema(schema);
		}

		public ChannelSchemaBuilder OfType(string type) {
            if (String.IsNullOrWhiteSpace(type))
                throw new ArgumentException($"'{nameof(type)}' cannot be null or whitespace.", nameof(type));

            schema.Type = type;
			return this;
		}

		public ChannelSchemaBuilder ByProvider(string provider) {
            if (String.IsNullOrWhiteSpace(provider))
                throw new ArgumentException($"'{nameof(provider)}' cannot be null or whitespace.", nameof(provider));

            schema.Provider = provider;
			return this;
		}

		public ChannelSchemaBuilder WithDirection(ChannelDirection direction) {
            if (direction == ChannelDirection.None)
                throw new ArgumentException($"'{nameof(direction)}' cannot be {nameof(ChannelDirection.None)}.", nameof(direction));

			schema.Directions = schema.Directions | direction;
			return this;
		}

        public ChannelSchemaBuilder WithInbound()
            => WithDirection(ChannelDirection.Inbound);

        public ChannelSchemaBuilder WithOutbound()
            => WithDirection(ChannelDirection.Outbound);

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
