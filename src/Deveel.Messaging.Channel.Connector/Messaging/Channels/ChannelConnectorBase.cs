using Deveel.Messaging.Channels;

namespace Deveel.Messaging {
	public abstract class ChannelConnectorBase : IChannelConnector, IDisposable {
		private bool disposedValue;

		private IDictionary<ConnectionKey, IChannelConnection>? connections;

		~ChannelConnectorBase() {
			Dispose(disposing: false);
		}

		protected abstract IChannelConnection CreateConnection(IChannel channel);

		protected virtual void Dispose(bool disposing) {
			if (!disposedValue) {
				if (disposing) {
					DisposeConnections();
				}

				connections = null;
				disposedValue = true;
			}
		}

		private void DisposeConnections() {
			if (connections != null) {
				foreach (var connection in connections) {
					if (connection.Value is IDisposable disposable)
						disposable.Dispose();
				}
				connections.Clear();
			}
		}

		public void Dispose() {
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}

		public IChannelConnection Connect(IChannel channel) {
			lock (this) {
				if (connections == null)
					connections = new Dictionary<ConnectionKey, IChannelConnection>();

				if (!connections.TryGetValue(new ConnectionKey(channel.Type, channel.Provider, channel.Id), out var connection)) {
					connection = CreateConnection(channel);
					connections.Add(new ConnectionKey(channel.Type, channel.Provider, channel.Id), connection);
				}

				return connection;
			}
		}

		#region ConnectionKey

		readonly struct ConnectionKey : IEquatable<ConnectionKey> {
			public ConnectionKey(string type, string provider, string channelId) {
				Type = type;
				Provider = provider;
				ChannelId = channelId;
			}

			public string Type { get; }

			public string Provider { get; }

			public string ChannelId { get; }

			public override bool Equals(object? obj) => obj is ConnectionKey key && Equals(key);

			public bool Equals(ConnectionKey other) => Type == other.Type && Provider == other.Provider && ChannelId == other.ChannelId;

			public override int GetHashCode() => HashCode.Combine(Type, Provider, ChannelId);
		}

		#endregion
	}
}
