namespace Deveel.Messaging {
	/// <summary>
	/// A service that represents a queue of messages
	/// to be processed.
	/// </summary>
	/// <remarks>
	/// <para>
	/// Message queues are used to distribute the processing
	/// across multiple threads and/or processes, and as such
	/// they must be reliable and thread-safe.
	/// </para>
	/// <para>
	/// Some implementations of this interface may provide
	/// a push mechanism to notify the availability of a new
	/// message in the queue, while others may provide a pull
	/// mechanism to retrieve the messages: in the latter case
	/// the implementation must provide a way to wait for a
	/// message to be available.
	/// </para>
	/// <para>
	/// The model of this queue is a FIFO (First-In-First-Out),
	/// and it is not inteded to be used as a persistent storage,
	/// or to keep track of the processing of the messages by
	/// the consumers.
	/// </para>
	/// </remarks>
	public interface IMessageQueue {
		/// <summary>
		/// Introduces a new message in the queue.
		/// </summary>
		/// <param name="message">
		/// The message to enqueue for processing.
		/// </param>
		/// <param name="cancellationToken">
		/// A token that can be used to cancel the enqueueing.
		/// </param>
		/// <returns>
		/// Returns a task that when completed will
		/// have enqueued the message.
		/// </returns>
		Task EnqueueAsync(IMessage message, CancellationToken cancellationToken = default);

		/// <summary>
		/// Waits for a message to be available in the queue,
		/// to be dequeued and processed.
		/// </summary>
		/// <param name="cancellationToken">
		/// A token that can be used to cancel the waiting.
		/// </param>
		/// <returns>
		/// Returns the first message available in the queue,
		/// or <c>null</c> if the waiting has been cancelled.
		/// </returns>
		Task<IMessage?> DequeueAsync(CancellationToken cancellationToken = default);
	}
}
