namespace Deveel.Messaging.Terminals {
	/// <summary>
	/// Extends the <see cref="IServiceTerminal"/> contract
	/// to provide some common functionalities.
	/// </summary>
	public static class ServiceTerminalExtensions {
		/// <summary>
		/// Checks if the given terminal is a sender.
		/// </summary>
		/// <param name="terminal">
		/// The terminal to check.
		/// </param>
		/// <returns>
		/// Returns <c>true</c> if the given terminal is a sender,
		/// otherwise <c>false</c>.
		/// </returns>
		public static bool IsSender(this IServiceTerminal terminal)
			=> terminal.Roles.HasFlag(TerminalRole.Sender);

		/// <summary>
		/// Checks if the given terminal is a receiver.
		/// </summary>
		/// <param name="terminal">
		/// The terminal to check.
		/// </param>
		/// <returns>
		/// Returns <c>true</c> if the given terminal is a receiver,
		/// otherwise <c>false</c>.
		/// </returns>
		public static bool IsReceiver(this IServiceTerminal terminal)
			=> terminal.Roles.HasFlag(TerminalRole.Receiver);

		public static bool IsActive(this IServiceTerminal terminal)
			=> terminal.Status == TerminalStatus.Active;

		public static bool IsDisabled(this IServiceTerminal terminal)
			=> terminal.Status == TerminalStatus.Disabled;
	}
}
