using Deveel.Messaging.Channels;

namespace Deveel.Messaging.Channels {
	/// <summary>
	/// An implementation of <see cref="IChannelConnector"/> that is used
	/// to run unit tests on the messaging system.
	/// </summary>
	/// <remarks>
	/// This connector supports sending and receiving messages,
	/// and also supports batching.
	/// </remarks>
	[Connector(TestChannelDefaults.Type, TestChannelDefaults.Provider)]
	public class TestConnector : ChannelConnectorBase {
		private readonly IMessageSendCallback? sendCallback;

		public TestConnector(IMessageSendCallback? sendCallback = null) {
			this.sendCallback = sendCallback;
		}

		protected override IChannelConnection CreateConnection(IChannel channel) => new TestConnection(this);

		private async Task<MessageResult> SendAsync(IMessage message, CancellationToken cancellationToken = default) {
			if (sendCallback == null)
				throw new NotSupportedException("The connector does not support sending messages.");

			cancellationToken.ThrowIfCancellationRequested();

			try {
				return await sendCallback.OnMessageSendingAsync(message);
			} catch (ChannelException ex) {
				return MessageResult.Fail(ex);
			}
		}

		class TestConnection : IChannelConnection {
			private readonly TestConnector connector;

			public TestConnection(TestConnector connector) {
				this.connector = connector;
			}

			public bool IsConnected => true;

			public IChannelReceiver CreateReceiver() => throw new NotImplementedException();

			public IChannelSender CreateSender() => new TestSender(connector);

			public void Dispose() {
			}
		}

		class TestSender : IChannelSender {
			private readonly TestConnector connector;

			public TestSender(TestConnector connector) {
				this.connector = connector;
			}

			public Task<MessageResult> SendAsync(IMessage message, CancellationToken cancellationToken = default) => connector.SendAsync(message, cancellationToken);
		}
	}
}
