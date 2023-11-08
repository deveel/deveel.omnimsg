namespace Deveel.Messaging.Terminals {
	/// <summary>
	/// Provides the options to use when resolving a terminal.
	/// </summary>
	public sealed class TerminalResolutionOptions {
		/// <summary>
		/// Gets or sets the identifier of the tenant that
		/// owns the terminal to resolve.
		/// </summary>
		public string? TenantId { get; set; }
	}
}
