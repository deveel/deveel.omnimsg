namespace Deveel.Messaging.Channels {
	public interface IMessageSendCallback {
		Task<MessageResult> OnMessageSendingAsync(IMessage message);
	}
}
