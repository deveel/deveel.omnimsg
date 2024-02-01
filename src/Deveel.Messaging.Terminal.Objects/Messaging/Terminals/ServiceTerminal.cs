namespace Deveel.Messaging.Terminals {
	/// <summary>
	/// An implementation of a <see cref="IServiceTerminal"/> that represents
	/// a terminal that is used to send or receive messages to/from a remote
	/// endpoint.
	/// </summary>
	public class ServiceTerminal : IServiceTerminal {
		public ServiceTerminal() {
		}

		/// <summary>
		/// Constructs the terminal from the given <paramref name="terminal"/>.
		/// </summary>
		/// <param name="terminal">
		/// The terminal to construct this instance from.
		/// </param>
		public ServiceTerminal(IServiceTerminal terminal) {
			Id = terminal.Id;
			TenantId = terminal.TenantId;
			Type = terminal.Type;
			Address = terminal.Address;
			Name = terminal.Name;
			Provider = terminal.Provider;
			Roles = terminal.Roles;
			Status = terminal.Status;
			Channels = terminal.Channels?.Select(x => new TerminalChannel(x)).ToList() ?? new List<TerminalChannel>();
			Context = terminal.Context?.ToDictionary(x => x.Key, x => x.Value) ?? new Dictionary<string, object>();
		}

		/// <inheritdoc/>
		public string Id { get; set; }

		/// <inheritdoc/>
		public string? TenantId { get; set; }

		/// <inheritdoc/>
		public string Type { get; set; }

		/// <inheritdoc/>
		public string Address { get; set; }

		/// <inheritdoc/>
		public string Name { get; set; }

		/// <inheritdoc/>
		public string Provider { get; set; }

		/// <inheritdoc/>
		public TerminalRole Roles { get; set; }

		/// <inheritdoc/>
		public TerminalStatus Status { get; set; }

		IEnumerable<ITerminalChannel>? IServiceTerminal.Channels => Channels;

		/// <inheritdoc/>
		public IList<TerminalChannel> Channels { get; set; }

		/// <inheritdoc/>
		public IDictionary<string, object> Context { get; set; }
	}
}