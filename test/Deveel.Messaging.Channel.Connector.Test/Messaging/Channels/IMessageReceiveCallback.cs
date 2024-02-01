namespace Deveel.Messaging.Channels {
	public interface IMessageReceiveCallback {
		Task<MessageReceiveResult> OnMessageReceivedAsync();
	}
}
