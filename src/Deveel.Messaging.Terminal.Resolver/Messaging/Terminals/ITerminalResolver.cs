namespace Deveel.Messaging.Terminals {
	/// <summary>
	/// A client service that can resolve terminals.
	/// </summary>
	public interface ITerminalResolver {
		/// <summary>
		/// Attempts to find a terminal by the given identifier.
		/// </summary>
		/// <param name="terminalId">
		/// The identifier of the terminal to find.
		/// </param>
		/// <param name="options">
		/// A set of options that can be used to resolve the terminal.
		/// </param>
		/// <param name="cancellationToken">
		/// A token that can be used to cancel the operation.
		/// </param>
		/// <returns>
		/// Returns an instance of <see cref="IServiceTerminal"/> if the terminal
		/// was found by the given identifier, otherwise <c>null</c>.
		/// </returns>
		Task<IServiceTerminal?> FindByIdAsync(string terminalId, TerminalResolutionOptions? options = null, CancellationToken cancellationToken = default);

		/// <summary>
		/// Attempts to find a terminal by the given type and address.
		/// </summary>
		/// <param name="type">
		/// The type of the terminal to find.
		/// </param>
		/// <param name="address">
		/// The address of the terminal to find.
		/// </param>
		/// <param name="options">
		/// A set of options that can be used to resolve the terminal.
		/// </param>
		/// <param name="cancellationToken">
		/// A token that can be used to cancel the operation.
		/// </param>
		/// <returns>
		/// Returns an instance of <see cref="IServiceTerminal"/> if the terminal
		/// was found by the given type and address, otherwise <c>null</c>.
		/// </returns>
		Task<IServiceTerminal?> FindByAddressAsync(string type, string address, TerminalResolutionOptions? options = null, CancellationToken cancellationToken = default);
	}
}