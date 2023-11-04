namespace Deveel.Messaging {
	/// <summary>
	/// A terminal is a node in a network that is able 
	/// to send or receive messages
	/// </summary>
	/// <remarks>
	/// <para>
	/// A terminal is specialized to a specific protocol
	/// for sending and receiving messages: this can be
	/// an HTTP endpoint (eg. a URL), an e-mail address,
	/// a TCP/IP address, etc.
	/// </para>
	/// <para>
	/// A type of terminal can be used by more than one
	/// protocol.
	/// </para>
	/// </remarks>
	public interface ITerminal {
		/// <summary>
		/// Gets the type of the terminal that is used.
		/// </summary>
		string Type { get; }

		/// <summary>
		/// Gets the address of the terminal.
		/// </summary>
		string Address { get; }
	}
}