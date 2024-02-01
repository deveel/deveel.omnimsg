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
	static class MessageSenderLogEventId {
		public const int UnknownSenderError = -200021;
		public const int UnknownStateError = -200022;

		public const int WarningMessageNotSent = -200031;
		public const int WarningCallbackError = -200032;

		public const int ChannelNotFoundError = -200123;
		public const int ChannelNotActiveError = -200124;
		public const int ChannelNotAvailableError = -200125;

		public const int TraceResolvingConnector = 2000210;
		public const int TraceResolvingChannelByName = 2000211;
		public const int TraceResolvingChannelById = 2000212;
		public const int TraceChannelResolved = 2000213;
		public const int TraceSendingMessage = 200121;
		public const int TraceMessageSent = 200122;
		public const int TraceMessageRetrySend = 2001531;

		public const int TraceStateReceived = 200221;
		public const int TraceStateRouting = 200222;
		public const int TraceStateRouted = 200223;
	}
}
