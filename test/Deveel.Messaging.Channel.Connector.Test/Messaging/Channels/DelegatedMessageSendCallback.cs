namespace Deveel.Messaging.Channels {
	public class DelegatedMessageSendCallback : IMessageSendCallback {
		private readonly Func<IMessage, CancellationToken, Task<MessageResult>>? sendCallback;

		public DelegatedMessageSendCallback(Func<IMessage, CancellationToken, Task<MessageResult>>? sendCallback = null) {
			this.sendCallback = sendCallback;
		}

		public Task<MessageResult> OnMessageSendingAsync(IMessage message) 
			=> sendCallback?.Invoke(message, CancellationToken.None) ?? Task.FromResult(MessageResult.Success(message));
	}
}
