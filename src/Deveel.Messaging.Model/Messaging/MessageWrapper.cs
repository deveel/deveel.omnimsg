namespace Deveel.Messaging {
	class MessageWrapper : IMessage {
		private readonly IMessage message;

		public MessageWrapper(IMessage message) {
			this.message = message;
		}

		public virtual string Id => message.Id;

		public virtual string TenantId => message.TenantId;

		public virtual ITerminal Sender => message.Sender;

		public virtual ITerminal Receiver => message.Receiver;

		public virtual IMessageChannel Channel => message.Channel;

		public virtual IMessageContent Content => message.Content;

		public virtual MessageDirection Direction => message.Direction;

		public virtual IDictionary<string, object>? Options => message.Options;

		public virtual IDictionary<string, object>? Properties => message.Properties;

		public virtual IDictionary<string, object>? Context => message.Context;

		public virtual DateTimeOffset TimeStamp => message.TimeStamp;
	}
}
