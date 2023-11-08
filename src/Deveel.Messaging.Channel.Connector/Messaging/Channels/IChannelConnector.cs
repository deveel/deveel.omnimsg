namespace Deveel.Messaging.Channels {
	/// <summary>
	/// A service that allows to connect to
	/// a channel by a provider of messaging services.
	/// </summary>
	public interface IChannelConnector {
		/// <summary>
		/// Establishes a connection to the given channel.
		/// </summary>
		/// <param name="channel">
		/// The configuration of the channel to connect to.
		/// </param>
		/// <returns>
		/// Returns an instance of <see cref="IChannelConnection"/> that can be
		/// used to send or receive messages from the channel.
		/// </returns>
		/// <exception cref="ArgumentException">
		/// Thrown if the given <paramref name="channel"/> is not
		/// compatible with the connector.
		/// </exception>
		IChannelConnection Connect(IChannel channel);
	}
}
