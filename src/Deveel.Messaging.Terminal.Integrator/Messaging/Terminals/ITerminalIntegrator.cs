namespace Deveel.Messaging.Terminals {
	public interface ITerminalIntegrator {
		Task AssociateChannelAsync(string terminalId, ITerminalChannel channel, CancellationToken cancellationToken = default);

		Task DissociateChannelAsync(string terminalId, ITerminalChannel channel, CancellationToken cancellationToken = default);

		Task<IServiceTerminal?> FindByIdAsync(string terminalId, CancellationToken cancellationToken = default);

		Task<IServiceTerminal?> FindByAddressAsync(string type, string address, CancellationToken cancellationToken = default);
	}
}
