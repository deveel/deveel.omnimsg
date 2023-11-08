namespace Deveel.Messaging.Channels {
	public interface IChannelConnectorResolver {
		IChannelConnector? Resolve(string type, string provider);
	}
}
