using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deveel.Messaging.Channels {
	public sealed class ConnectorDescriptor {
		public ConnectorDescriptor(string type, string provider, Type connectorType) {
			ArgumentNullException.ThrowIfNull(type, nameof(type));
			ArgumentNullException.ThrowIfNull(provider, nameof(provider));
			ArgumentNullException.ThrowIfNull(connectorType, nameof(connectorType));

			if (!typeof(IChannelConnector).IsAssignableFrom(connectorType))
				throw new ArgumentException($"The type {connectorType} is not a valid channel connector type.");

			Type = type;
			Provider = provider;
			ConnectorType = connectorType;
		}

		public string Type { get; }

		public string Provider { get; }

		public Type ConnectorType { get; }
	}
}
