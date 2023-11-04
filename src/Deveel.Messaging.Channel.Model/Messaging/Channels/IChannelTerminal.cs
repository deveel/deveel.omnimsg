namespace Deveel.Messaging.Channels {
	/// <summary>
	/// Rpresents a reference to a terminal that is
	/// bound to a channel.
	/// </summary>
	public interface IChannelTerminal : ITerminal {
		/// <summary>
		/// Gets the unique identifier of the terminal
		/// in the scope of the application.
		/// </summary>
		string Id { get; }
	}
}
