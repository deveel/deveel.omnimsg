namespace Deveel.Messaging.Channels {
	class ChannelWrapper : IChannel {
		private readonly IChannel channel;

		public ChannelWrapper(IChannel channel) {
			this.channel = channel;
		}

		public virtual string Type => channel.Type;

		public virtual string Provider => channel.Provider;

		public virtual string Name => channel.Name;

		public virtual ChannelDirection Directions => channel.Directions;

		public virtual IEnumerable<IChannelTerminal>? Terminals => channel.Terminals;

		public virtual IEnumerable<string> ContentTypes => channel.ContentTypes;

		public virtual IDictionary<string, object>? Options => channel.Options;

		public virtual IEnumerable<IChannelCredentials>? Credentials => channel.Credentials;

		public virtual ChannelStatus Status => channel.Status;

		public virtual string Id => channel.Id;

		public virtual string? TenantId => channel.TenantId;

		public virtual IDictionary<string, object> Context => channel.Context;
	}
}
