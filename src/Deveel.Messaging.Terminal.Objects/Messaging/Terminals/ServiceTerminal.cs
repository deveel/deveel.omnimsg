namespace Deveel.Messaging.Terminals {
	public class ServiceTerminal : IServiceTerminal {
		public string Id { get; set; }

		public string? TenantId { get; set; }

		public string Type { get; set; }

		public string Address { get; set; }

		public string Name { get; set; }

		public string Provider { get; set; }

		public TerminalRole Roles { get; set; }

		public TerminalStatus Status { get; set; }

		public IEnumerable<ITerminalChannel>? Channels { get; }

		public IDictionary<string, object> Context { get; }
	}
}