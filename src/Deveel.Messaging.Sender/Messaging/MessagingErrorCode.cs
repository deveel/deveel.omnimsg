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

namespace Deveel.Messaging {
	public static class MessagingErrorCode {
		public const string ChannelNotFound = "channel.not_found";
		public const string ChannelNotSupported = "channel.not_supported";
		public const string ChannelNotActive = "channel.not_active";
		public const string ChannelTimeout = "channel.timeout";
		public const string ChannelUnknownError = "channel.unknown_error";
		public const string ChannelNotAvailable = "channel.not_available";

		public const string TerminalNotFound = "terminal.not_found";
		public const string TerminalNotSupported = "terminal.not_supported";
		public const string TerminalNotActive = "terminal.not_active";

		public const string UnknownError = "message.unknown_error";
	}
}
