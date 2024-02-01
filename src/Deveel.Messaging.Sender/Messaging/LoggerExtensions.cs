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

using Microsoft.Extensions.Logging;

namespace Deveel.Messaging {
	static partial class LoggerExtensions {
		[LoggerMessage(EventId = MessageSenderLogEventId.UnknownSenderError,
			Level = LogLevel.Error, Message = "Unknown error while sending messages")]
		public static partial void LogUnknownSenderError(this ILogger logger, Exception exception);

		[LoggerMessage(EventId = MessageSenderLogEventId.ChannelNotFoundError,
			Level = LogLevel.Error, Message = "The {ChannelType} channel by {ChannelProvider} named '{ChannelName}' was not found")]
		public static partial void LogChannelNotFound(this ILogger logger, string channelType, string channelProvider, string channelName);

		[LoggerMessage(EventId = MessageSenderLogEventId.ChannelNotAvailableError,
			Level = LogLevel.Error, Message = "The {ChannelType} channel by {ChannelProvider} is not supported")]
		public static partial void LogChannelNotAvailable(this ILogger logger, string channelType, string channelProvider);

		[LoggerMessage(EventId = MessageSenderLogEventId.ChannelNotActiveError,
			Level = LogLevel.Error, Message = "The {ChannelType} channel by {ChannelProvider} named '{ChannelName}' is not active")]
		public static partial void LogChannelNotActive(this ILogger logger, string channelType, string channelProvider, string channelName);

		[LoggerMessage(EventId = MessageSenderLogEventId.TraceResolvingConnector,
			Level = LogLevel.Debug, Message = "Resolving {ChannelType} channel connector by {ChannelProvider}")]
		public static partial void TraceResolvingConnector(this ILogger logger, string channelType, string channelProvider);

		[LoggerMessage(EventId = MessageSenderLogEventId.TraceResolvingChannelByName,
			Level = LogLevel.Debug, Message = "Resolving channel named '{ChannelName}'")]
		public static partial void TraceResolvingChannelByName(this ILogger logger, string channelName);

		[LoggerMessage(EventId = MessageSenderLogEventId.TraceResolvingChannelById,
			Level = LogLevel.Debug, Message = "Resolving channel by id '{ChannelId}'")]
		public static partial void TraceResolvingChannelById(this ILogger logger, string channelId);

		[LoggerMessage(EventId = MessageSenderLogEventId.TraceChannelResolved,
			Level = LogLevel.Debug, Message = "Channel {ChannelType} by {ChannelProvider} named '{ChannelName}'")]
		public static partial void TraceChannelResolved(this ILogger logger, string channelType, string channelProvider, string channelName);

		[LoggerMessage(EventId = MessageSenderLogEventId.TraceSendingMessage,
			Level = LogLevel.Debug, Message = "Sending message {MessageId} to channel {ChannelType} by {ChannelProvider} named '{ChannelName}'")]
		public static partial void TraceSendingMessage(this ILogger logger, string messageId, string channelType, string channelProvider, string channelName);

		[LoggerMessage(EventId = MessageSenderLogEventId.TraceMessageSent,
			Level = LogLevel.Information, Message = "Message {MessageId} sent to channel {ChannelType} by {ChannelProvider} named '{ChannelName}'")]
		public static partial void TraceMessageSent(this ILogger logger, string messageId, string channelType, string channelProvider, string channelName);

		[LoggerMessage(EventId = MessageSenderLogEventId.WarningMessageNotSent,
			Level = LogLevel.Warning, Message = "Message {MessageId} was not sent to channel {ChannelType} by {ChannelProvider} named '{ChannelName}' - [{ErrorCode}] {ErrorMessage}")]
		public static partial void WarnMessageNotSent(this ILogger logger, string messageId, string channelType, string channelProvider, string channelName, string errorCode, string? errorMessage);

		[LoggerMessage(EventId = MessageSenderLogEventId.WarningCallbackError,
			Level = LogLevel.Warning, Message = "Error while handling the status {MessageStatus} of message {MessageId}")]
		public static partial void WarnCallbackError(this ILogger logger, Exception exception, string messageId, MessageStatus messageStatus);

		[LoggerMessage(EventId = MessageSenderLogEventId.TraceMessageRetrySend,
			Level = LogLevel.Information, Message = "Message {MessageId} will be retried to be sent to channel {ChannelType} by {ChannelProvider} named '{ChannelName}'")]
		public static partial void TraceMessageRetrySend(this ILogger logger, string messageId, string channelType, string channelProvider, string channelName);
	}
}
