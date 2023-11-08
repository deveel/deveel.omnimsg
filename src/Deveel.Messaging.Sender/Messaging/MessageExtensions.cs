using Deveel.Messaging.Channels;

using Microsoft.Extensions.Options;

namespace Deveel.Messaging {
	static class MessageExtensions {
		public static TimeSpan Timeout(this IMessage message, IChannel channel, int? defaultTimeoutMs = null) {
			var timeoutMs = message.Timeout();
			if (timeoutMs == null)
				timeoutMs = channel.Timeout();
			if (timeoutMs == null)
				timeoutMs = defaultTimeoutMs;

			return timeoutMs != null ? TimeSpan.FromMilliseconds(timeoutMs.Value) : System.Threading.Timeout.InfiniteTimeSpan;
		}


		public static int RetryCount(this IMessage message, IChannel channel, int? defaultMaxRetries = null) {
			if ((channel.HasRetry() && channel.Retry() == false) ||
				(message.HasRetry() && message.Retry() == false))
				return 0;

			var retryCount = message.RetryCount() ?? channel.RetryCount();

			if (retryCount == null)
				retryCount = defaultMaxRetries;

			return retryCount ?? 0;
		}
	}
}
