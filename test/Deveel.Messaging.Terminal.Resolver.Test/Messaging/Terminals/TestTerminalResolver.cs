using System.Collections.Concurrent;

namespace Deveel.Messaging.Terminals {
	public sealed class TestTerminalResolver : ITerminalResolver {
		private readonly ConcurrentDictionary<string, List<IServiceTerminal>> terminals;

		public TestTerminalResolver(IEnumerable<IServiceTerminal>? terminals = null) {
			if (terminals == null) {
				this.terminals = new ConcurrentDictionary<string, List<IServiceTerminal>>();
			} else {
				var grouped = terminals.GroupBy(x => x.TenantId ?? "");
				this.terminals = new ConcurrentDictionary<string, List<IServiceTerminal>>(grouped.ToDictionary(x => x.Key, x => x.ToList()));
			}
		}

		public IReadOnlyList<IServiceTerminal> Terminals => terminals.Values.SelectMany(x => x).ToList();

		public Task<IServiceTerminal?> FindByAddressAsync(string type, string address, TerminalResolutionOptions? options = null, CancellationToken cancellationToken = default) {
			var tenantId = options?.TenantId ?? "";

			if (!terminals.TryGetValue(tenantId, out var list))
				return Task.FromResult<IServiceTerminal?>(null);

			return Task.FromResult(list.FirstOrDefault(x => x.Address == address));
		}

		public Task<IServiceTerminal?> FindByIdAsync(string terminalId, TerminalResolutionOptions? options = null, CancellationToken cancellationToken = default) {
			var tenantId = options?.TenantId ?? "";

			if (!terminals.TryGetValue(tenantId, out var list))
				return Task.FromResult<IServiceTerminal?>(null);

			return Task.FromResult(list.FirstOrDefault(x => x.Id == terminalId));
		}
	}
}
