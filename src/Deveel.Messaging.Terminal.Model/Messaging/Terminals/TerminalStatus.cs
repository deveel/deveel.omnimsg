namespace Deveel.Messaging.Terminals {
	/// <summary>
	/// Enumerates the possible statuses of a service terminal.
	/// </summary>
	public enum TerminalStatus {
		/// <summary>
		/// The status of the terminal is unknown.
		/// </summary>
		Unknown = 0,

		/// <summary>
		/// The terminal is active and available 
		/// for communication.
		/// </summary>
		Active = 1,

		/// <summary>
		/// The terminal is disabled and not available 
		/// for communication.
		/// </summary>
		Disabled = 2,
	}
}
