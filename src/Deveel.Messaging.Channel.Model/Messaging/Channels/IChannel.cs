namespace Deveel.Messaging.Channels {
	/// <summary>
	/// An instance of a messaging channel that is used
	/// to transport messages through a network.
	/// </summary>
	public interface IChannel : IMessageContextProvider {
		/// <summary>
		/// Gets the unique identifier of the channel
		/// instance in the scope of the network.
		/// </summary>
		string Id { get; }

		/// <summary>
		/// Gets the type of the channel.
		/// </summary>
		string Type { get; }

		/// <summary>
		/// Gets the identifier of the provider of
		/// messaging services, to which the channel
		/// is connected.
		/// </summary>
		string Provider { get; }

		/// <summary>
		/// Gets the descriptive name of the channel.
		/// </summary>
		string Name { get; }

		/// <summary>
		/// Gets the routing directions supported
		/// by the channel.
		/// </summary>
		ChannelDirection Directions { get; }

		/// <summary>
		/// Gets a reference to the terminals that are
		/// bound to the channel, as stable senders or 
		/// receivers.
		/// </summary>
		IEnumerable<IChannelTerminal> Terminals { get; }

		/// <summary>
		/// Gets a set of content types that are supported
		/// by this instance of channel.
		/// </summary>
		IEnumerable<string> ContentTypes { get; }

		/// <summary>
		/// Gets a set of options that are used to configure
		/// the behavior of the channel.
		/// </summary>
		IDictionary<string, object> Options { get; }

		/// <summary>
		/// Gets a set of credentials that are used to
		/// authenticate the channel towards the network.
		/// </summary>
		IEnumerable<IChannelCredentials> Credentials { get; }

		/// <summary>
		/// Gets the current status of the channel.
		/// </summary>
		ChannelStatus Status { get; }
	}
}
