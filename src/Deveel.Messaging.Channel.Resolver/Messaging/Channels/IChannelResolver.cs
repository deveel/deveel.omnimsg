namespace Deveel.Messaging.Channels {
	/// <summary>
	/// A client service that is able to resolve a channel.
	/// </summary>
	public interface IChannelResolver {
		/// <summary>
		/// Finds a channel by its identifier.
		/// </summary>
		/// <param name="channelId">
		/// The identifier of the channel to find.
		/// </param>
		/// <param name="cancellationToken">
		/// A cancellation token that can be used to cancel the operation.
		/// </param>
		/// <returns>
		/// Returns an instance of <see cref="IChannel"/> if a channel
		/// was found with the given identifier, otherwise <c>null</c>.
		/// </returns>
		Task<IChannel?> FindByIdAsync(string channelId, ChannelResolutionOptions? options = null, CancellationToken cancellationToken = default);

		/// <summary>
		/// Attempts to find a channel by its name.
		/// </summary>
		/// <param name="channelName">
		/// The name of the channel to find.
		/// </param>
		/// <param name="cancellationToken">
		/// A cancellation token that can be used to cancel the operation.
		/// </param>
		/// <returns>
		/// Returns an instance of <see cref="IChannel"/> if a channel
		/// was found with the given name, otherwise <c>null</c>.
		/// </returns>
		Task<IChannel?> FindByNameAsync(string channelName, ChannelResolutionOptions? options = null, CancellationToken cancellationToken = default);
	}
}