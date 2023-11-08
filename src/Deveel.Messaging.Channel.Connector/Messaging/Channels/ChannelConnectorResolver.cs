namespace Deveel.Messaging.Channels {
	public class ChannelConnectorResolver : IChannelConnectorResolver {
		private readonly IServiceProvider serviceProvider;
		private readonly IEnumerable<ConnectorDescriptor> descriptors;

		public ChannelConnectorResolver(IServiceProvider serviceProvider, IEnumerable<ConnectorDescriptor> descriptors) {
			this.serviceProvider = serviceProvider;
			this.descriptors = descriptors;
		}

		protected virtual ConnectorDescriptor? GetDescriptor(string type, string provider) {
			return descriptors.FirstOrDefault(x => x.Type == type && x.Provider == provider);
		}

		public IChannelConnector? Resolve(string type, string provider) {
			var descriptor = GetDescriptor(type, provider);

			if (descriptor == null)
				return null;

			return (IChannelConnector?) serviceProvider.GetService(descriptor.ConnectorType);
		}
	}
}
