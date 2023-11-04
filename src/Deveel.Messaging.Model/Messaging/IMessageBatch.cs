namespace Deveel.Messaging {
	/// <summary>
	/// A batch of messages that are exchanged between two terminals.
	/// </summary>
	public interface IMessageBatch : IMessageContextProvider {
		/// <summary>
		/// Gets the unique identifier of the batch.
		/// </summary>
		string Id { get; }

		/// <summary>
		/// Gets a set of options that are used to configure the
		/// behaviors of the channel when handling the messages
		/// included in the batch.
		/// </summary>
		/// <remarks>
		/// Batch-level options can be used to override the default
		/// options defined at the channel level, but they can also
		/// be overridden by the message-level options.
		/// </remarks>
		/// <seealso cref="IMessage.Options"/>
		IDictionary<string, object>? Options { get; }

		/// <summary>
		/// Gets the messages included in the batch.
		/// </summary>
		IEnumerable<IMessage> Messages { get; }
	}
}
