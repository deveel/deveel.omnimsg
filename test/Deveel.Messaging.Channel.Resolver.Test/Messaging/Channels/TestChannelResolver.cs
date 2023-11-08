using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deveel.Messaging.Channels {
	public sealed class TestChannelResolver : IChannelResolver {
		private readonly ConcurrentDictionary<string, List<IChannel>> channels;

		public TestChannelResolver(IEnumerable<IChannel>? channels = null) {
			if (channels != null) {
				var grouped = channels.GroupBy(x => x.TenantId ?? "");
				this.channels = new ConcurrentDictionary<string, List<IChannel>>(grouped.ToDictionary(x => x.Key, x => x.ToList()));
			} else {
				this.channels = new ConcurrentDictionary<string, List<IChannel>>();
			}
		}

		public IReadOnlyList<IChannel> Channels => channels.Values.SelectMany(x => x).ToList();

		public Task<IChannel?> FindByIdAsync(string id, ChannelResolutionOptions? options = null, CancellationToken cancellationToken = default) {
			var tenantId = options?.TenantId ?? "";

			if (!channels.TryGetValue(tenantId, out var list))
				return Task.FromResult<IChannel?>(null);

			var channel = list.FirstOrDefault(x => x.Id == id);

			if (channel != null && options != null && !(options.IncludeCredentials ?? false))
				channel = channel.WithoutCredentials();

			return Task.FromResult(channel);
		}

		public Task<IChannel?> FindByNameAsync(string name, ChannelResolutionOptions? options = null, CancellationToken cancellationToken = default) {
			var tenantId = options?.TenantId ?? "";
			if (!channels.TryGetValue(tenantId, out var list))
				return Task.FromResult<IChannel?>(null);

			var channel = list.FirstOrDefault(x => x.Name == name);

			if (channel != null && options != null && !(options.IncludeCredentials ?? false))
				channel = channel.WithoutCredentials();

			return Task.FromResult(channel);
		}
	}
}