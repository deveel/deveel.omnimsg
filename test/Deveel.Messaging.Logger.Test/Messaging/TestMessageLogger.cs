namespace Deveel.Messaging {
	class TestMessageLogger : IMessageLogger {
		private readonly Func<IMessage, CancellationToken, Task>? messageLogger;
		private readonly Func<IMessageState, CancellationToken, Task>? stateLogger;

		public TestMessageLogger(Func<IMessage, CancellationToken, Task>? messageLogger = null, Func<IMessageState, CancellationToken, Task>? stateLogger = null) {
			this.messageLogger = messageLogger;
			this.stateLogger = stateLogger;
		}

		public Task LogMessageAsync(IMessage message, CancellationToken cancellationToken = default) {
			if (messageLogger == null)
				return Task.CompletedTask;

			return messageLogger(message, cancellationToken);
		}

		public Task LogMessageStateAsync(IMessageState state, CancellationToken cancellationToken = default) {
			if (stateLogger == null)
				return Task.CompletedTask;

			return stateLogger(state, cancellationToken);
		}
	}
}
