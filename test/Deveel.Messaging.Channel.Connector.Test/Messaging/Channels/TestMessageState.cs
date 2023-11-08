namespace Deveel.Messaging {
	public sealed class TestMessageState : IMessageState {
		public TestMessageState(string tenantId, string messageId, MessageStatus status, string? id = null, DateTimeOffset? timestamp = null) {
			if (string.IsNullOrWhiteSpace(tenantId))
				throw new ArgumentException($"'{nameof(tenantId)}' cannot be null or whitespace.", nameof(tenantId));
			if (string.IsNullOrWhiteSpace(messageId))
				throw new ArgumentException($"'{nameof(messageId)}' cannot be null or whitespace.", nameof(messageId));
			if (status == MessageStatus.Unknown)
				throw new ArgumentOutOfRangeException(nameof(status), status, "The status of the message cannot be unknown.");

			TenantId = tenantId;
			MessageId = messageId;
			Status = status;
			Id = id ?? Guid.NewGuid().ToString();
			TimeStamp = timestamp ?? DateTimeOffset.UtcNow;
		}

		public string Id { get; }

		public string TenantId { get; }

		public string MessageId { get; }

		public MessageStatus Status { get; }

		public IMessageError? Error { get; set; }

		public IMessageError? RemoteError { get; set; }

		public DateTimeOffset TimeStamp { get; }

		public IDictionary<string, object> Properties { get; set; } = new Dictionary<string, object>();
	}
}
