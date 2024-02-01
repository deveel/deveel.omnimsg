namespace Deveel.Messaging.Terminals {
	/// <summary>
	/// An implementation of a <see cref="ITerminalChannel"/> that represents
	/// a reference to a channel that is using the terminal.
	/// </summary>
	public class TerminalChannel : ITerminalChannel {
		/// <summary>
		/// Constructs a terminal channel with the given name and
		/// reference identifier.
		/// </summary>
		/// <param name="id"></param>
		/// <param name="name"></param>
		public TerminalChannel(string? id, string name) {
			Id = id;
			Name = name;
		}

		/// <summary>
		/// Constructs a terminal channel from the given channel.
		/// </summary>
		/// <param name="channel"></param>
		public TerminalChannel(ITerminalChannel channel) {
			Id = channel.Id;
			Name = channel.Name;
		}

		/// <summary>
		/// Constructs an empty terminal channel.
		/// </summary>
		public TerminalChannel() {
		}

		/// <inheritdoc/>
		public string? Id { get; set; }

		/// <inheritdoc/>
		public string Name { get; set; }
	}
}
