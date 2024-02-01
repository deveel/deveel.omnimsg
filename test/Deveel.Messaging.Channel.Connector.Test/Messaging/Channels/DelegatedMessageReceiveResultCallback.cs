namespace Deveel.Messaging.Channels {
	class DelegatedMessageReceiveResultCallback : IMessageReceiveCallback {
		private readonly Func<CancellationToken, Task<MessageReceiveResult>> _callback;

		public DelegatedMessageReceiveResultCallback(Func<CancellationToken, Task<MessageReceiveResult>> callback) {
			_callback = callback;
		}

		public Task<MessageReceiveResult> OnMessageReceivedAsync() 
			=> _callback?.Invoke(CancellationToken.None) ?? Task.FromResult(MessageReceiveResult.Failed());
	}
}
