namespace Deveel.Messaging.Channels {
	/// <summary>
	/// Represents a source of messages that can be received
	/// from a channel.
	/// </summary>
	public interface IMessageSource {
		/// <summary>
		/// Gets the format of the message that is expected
		/// to be received from the source.
		/// </summary>
		string MessageFormat { get; }

		/// <summary>
		/// Gets the type of channel this source
		/// belongs to.
		/// </summary>
		string Type { get; }

		/// <summary>
		/// Gets the identifier of the provider of
		/// the channel.
		/// </summary>
		string Provider { get; }

		/// <summary>
		/// Attempts to read the message from the source.
		/// </summary>
		/// <param name="cancellationToken">
		/// A cancellation token that can be used to cancel
		/// the operation.
		/// </param>
		/// <returns>
		/// Returns an instance of <see cref="IMessage"/> that
		/// is read from the source, or <c>null</c> if no message
		/// could be read from the source.
		/// </returns>
		Task<IMessage?> ReadAsMessageAsync(CancellationToken cancellationToken = default);
	}
}
