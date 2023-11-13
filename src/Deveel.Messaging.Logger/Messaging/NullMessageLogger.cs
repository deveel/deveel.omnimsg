namespace Deveel.Messaging {
	/// <summary>
	/// An instance of a <see cref="IMessageLogger"/> that does nothing.
	/// </summary>
	public sealed class NullMessageLogger : IMessageLogger {
		private NullMessageLogger() {
		}

		/// <summary>
		/// The singleton instance of the logger.
		/// </summary>
		public static readonly NullMessageLogger Instance = new NullMessageLogger();

		/// <inheritdoc/>
		public Task LogMessageAsync(IMessage message, CancellationToken cancellationToken = default) {
			return Task.CompletedTask;
		}

		/// <inheritdoc/>
		public Task LogMessageStateAsync(IMessageState state, CancellationToken cancellationToken = default) {
			return Task.CompletedTask;
		}
	}
}
