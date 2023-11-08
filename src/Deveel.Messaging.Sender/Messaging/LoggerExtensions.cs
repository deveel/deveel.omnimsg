﻿using Microsoft.Extensions.Logging;

namespace Deveel.Messaging {
	static partial class LoggerExtensions {
		[LoggerMessage(EventId = MessageSenderLogEventId.UnknownSenderError,
			Level = LogLevel.Error, Message = "Unknown error while sending messages")]
		public static partial void LogUnknownSenderError(this ILogger logger, Exception exception);

		[LoggerMessage(EventId = MessageSenderLogEventId.ChannelNotFoundError,
			Level = LogLevel.Error, Message = "The {ChannelType} channel by {ChannelProvider} named '{ChannelName}' was not found for tenant '{TenantId}'")]
		public static partial void LogChannelNotFound(this ILogger logger, string tenantId, string channelType, string channelProvider, string channelName);

		[LoggerMessage(EventId = MessageSenderLogEventId.ChannelNotAvailableError,
			Level = LogLevel.Error, Message = "The {ChannelType} channel by {ChannelProvider} is not supported")]
		public static partial void LogChannelNotAvailable(this ILogger logger, string channelType, string channelProvider);

		[LoggerMessage(EventId = MessageSenderLogEventId.ChannelNotActiveError,
			Level = LogLevel.Error, Message = "The {ChannelType} channel by {ChannelProvider} named '{ChannelName}' is not active for tenant '{TenantId}'")]
		public static partial void LogChannelNotActive(this ILogger logger, string tenantId, string channelType, string channelProvider, string channelName);

		[LoggerMessage(EventId = MessageSenderLogEventId.TraceResolvingConnector,
			Level = LogLevel.Debug, Message = "Resolving {ChannelType} channel connector by {ChannelProvider}")]
		public static partial void TraceResolvingConnector(this ILogger logger, string channelType, string channelProvider);

		[LoggerMessage(EventId = MessageSenderLogEventId.TraceResolvingChannelByName,
			Level = LogLevel.Debug, Message = "Resolving channel named '{ChannelName}' for tenant '{TenantId}'")]
		public static partial void TraceResolvingChannelByName(this ILogger logger, string tenantId, string channelName);

		[LoggerMessage(EventId = MessageSenderLogEventId.TraceResolvingChannelById,
			Level = LogLevel.Debug, Message = "Resolving channel by id '{ChannelId}' for tenant '{TenantId}'")]
		public static partial void TraceResolvingChannelById(this ILogger logger, string tenantId, string channelId);

		[LoggerMessage(EventId = MessageSenderLogEventId.TraceChannelResolved,
			Level = LogLevel.Debug, Message = "Channel {ChannelType} by {ChannelProvider} named '{ChannelName}' for tenant '{TenantId}'")]
		public static partial void TraceChannelResolved(this ILogger logger, string tenantId, string channelType, string channelProvider, string channelName);

		[LoggerMessage(EventId = MessageSenderLogEventId.TraceSendingMessage,
			Level = LogLevel.Debug, Message = "Sending message {MessageId} to channel {ChannelType} by {ChannelProvider} named '{ChannelName}' for tenant '{TenantId}'")]
		public static partial void TraceSendingMessage(this ILogger logger, string tenantId, string messageId, string channelType, string channelProvider, string channelName);

		[LoggerMessage(EventId = MessageSenderLogEventId.TraceMessageSent,
			Level = LogLevel.Information, Message = "Message {MessageId} sent to channel {ChannelType} by {ChannelProvider} named '{ChannelName}' for tenant '{TenantId}'")]
		public static partial void TraceMessageSent(this ILogger logger, string tenantId, string messageId, string channelType, string channelProvider, string channelName);

		[LoggerMessage(EventId = MessageSenderLogEventId.WarningMessageNotSent,
			Level = LogLevel.Warning, Message = "Message {MessageId} was not sent to channel {ChannelType} by {ChannelProvider} named '{ChannelName}' for tenant '{TenantId}' - [{ErrorCode}] {ErrorMessage}")]
		public static partial void WarnMessageNotSent(this ILogger logger, string tenantId, string messageId, string channelType, string channelProvider, string channelName, string errorCode, string? errorMessage);

		[LoggerMessage(EventId = MessageSenderLogEventId.WarningCallbackError,
			Level = LogLevel.Warning, Message = "Error while handling the status {MessageStatus} of message {MessageId} for tenant '{TenantId}'")]
		public static partial void WarnCallbackError(this ILogger logger, Exception exception, string tenantId, string messageId, MessageStatus messageStatus);

		[LoggerMessage(EventId = MessageSenderLogEventId.TraceMessageRetrySend,
			Level = LogLevel.Information, Message = "Message {MessageId} will be retried to be sent to channel {ChannelType} by {ChannelProvider} named '{ChannelName}' for tenant '{TenantId}'")]
		public static partial void TraceMessageRetrySend(this ILogger logger, string tenantId, string messageId, string channelType, string channelProvider, string channelName);
	}
}