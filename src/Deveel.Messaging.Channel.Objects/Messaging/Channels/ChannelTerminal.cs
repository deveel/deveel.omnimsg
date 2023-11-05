namespace Deveel.Messaging.Channels {
	public sealed class ChannelTerminal : IChannelTerminal {
		public ChannelTerminal(string type, string address, string? id = null) {
			Type = type;
			Address = address;
			Id = id;
		}

		public ChannelTerminal() {
		}

		public ChannelTerminal(IChannelTerminal terminal) {
			Id = terminal.Id;
			Type = terminal.Type;
			Address = terminal.Address;
		}

		/// <inheritdoc/>
		public string? Id { get; set; }

		/// <inheritdoc/>
		public string Type { get; set; }

		/// <inheritdoc/>
		public string Address { get; set; }
	}
}
