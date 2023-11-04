namespace Deveel.Messaging.Channels {
	/// <summary>
	/// The status of a channel in the network.
	/// </summary>
	public enum ChannelStatus {
		/// <summary>
		/// The status of the channel is unknown.
		/// </summary>
		Unknown = 0,

		/// <summary>
		/// The channel is active and can be used
		/// to send and receive messages.
		/// </summary>
		Active = 1,

		/// <summary>
		/// The channel is disabled and cannot be used
		/// to send or receive messages.
		/// </summary>
		Disabled = 2
	}
}
