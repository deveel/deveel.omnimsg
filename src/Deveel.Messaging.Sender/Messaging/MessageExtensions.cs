// Copyright 2023 Deveel AS
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

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
