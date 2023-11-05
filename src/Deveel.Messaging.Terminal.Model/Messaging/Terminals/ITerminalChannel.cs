namespace Deveel.Messaging.Terminals {
	/// <summary>
	/// Represents a reference to a channel that is
	/// bound to a service terminal.
	/// </summary>
	public interface ITerminalChannel {
		/// <summary>
		/// Gets the unique identifier of the channel.
		/// </summary>
		string? Id { get; }

		/// <summary>
		/// Gets the name of the channel.
		/// </summary>
		string Name { get; }
	}
}
