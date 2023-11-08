using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deveel.Messaging {
	public static class MessagingErrorCode {
		public const string ChannelNotFound = "channel.not_found";
		public const string ChannelNotSupported = "channel.not_supported";
		public const string ChannelNotActive = "channel.not_active";
		public const string ChannelTimeout = "channel.timeout";
		public const string ChannelUnknownError = "channel.unknown_error";
		public const string ChannelNotAvailable = "channel.not_available";

		public const string UnknownError = "message.unknown_error";
	}
}
