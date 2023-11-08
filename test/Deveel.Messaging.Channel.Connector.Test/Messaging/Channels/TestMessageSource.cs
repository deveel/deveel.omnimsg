namespace Deveel.Messaging.Channels {
	public class TestMessageSource : IMessageSource {
		public TestMessageSource(IMessage message) {
			Message = message;
		}

		public IMessage? Message { get; set; }

		string IMessageSource.MessageFormat => TestChannelDefaults.SourceFormat;
	}
}
