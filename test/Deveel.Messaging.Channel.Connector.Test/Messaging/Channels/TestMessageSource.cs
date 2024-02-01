
namespace Deveel.Messaging.Channels {
	public class TestMessageSource : IMessageSource {
		public TestMessageSource(IMessage message) {
			Message = message;
		}

		public IMessage? Message { get; set; }

		string IMessageSource.MessageFormat => TestChannelDefaults.SourceFormat;

		string IMessageSource.Type => TestChannelDefaults.Type;

		string IMessageSource.Provider => TestChannelDefaults.Provider;

		public Task<IMessage?> ReadAsMessageAsync(CancellationToken cancellationToken = default) => Task.FromResult(Message);
	}
}
