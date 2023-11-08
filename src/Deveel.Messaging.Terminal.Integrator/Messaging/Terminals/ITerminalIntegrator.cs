namespace Deveel.Messaging.Terminals {
	/// <summary>
	/// A client service that provides capabilities to integrate
	/// the terminal service.
	/// </summary>
	public interface ITerminalIntegrator {
		/// <summary>
		/// Associates a channel to the terminal identified 
		/// by the given identifier.
		/// </summary>
		/// <param name="terminalId">
		/// The identifier of the terminal to associate the channel to.
		/// </param>
		/// <param name="channel">
		/// The reference to the channel to associate.
		/// </param>
		/// <param name="cancellationToken">
		/// A token that can be used to cancel the operation.
		/// </param>
		/// <returns>
		/// Returns a task that when completed will have associated the
		/// given channel to the terminal.
		/// </returns>
		Task AssociateChannelAsync(string terminalId, ITerminalChannel channel, CancellationToken cancellationToken = default);

		/// <summary>
		/// Removes the association of the given channel to the terminal
		/// identified by the given identifier.
		/// </summary>
		/// <param name="terminalId">
		/// The identifier of the terminal to remove the association from.
		/// </param>
		/// <param name="channel">
		/// The reference to the channel to remove the association from.
		/// </param>
		/// <param name="cancellationToken">
		/// A token that can be used to cancel the operation.
		/// </param>
		/// <returns>
		/// Returns a task that when completed will have removed the association
		/// from the given channel to the terminal.
		/// </returns>
		Task DissociateChannelAsync(string terminalId, ITerminalChannel channel, CancellationToken cancellationToken = default);
	}
}
