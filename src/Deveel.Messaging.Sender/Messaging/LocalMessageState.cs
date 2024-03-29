﻿// Copyright 2023 Deveel AS
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
	/// <summary>
	/// A special implementation of <see cref="IMessageState"/> that is used
	/// to represent the state of a message that is generated locally,
	/// before being sent to a remote channel, or after being received from
	/// a remote channel.
	/// </summary>
	public sealed class LocalMessageState : IMessageState {
		/// <summary>
		/// Constructs the state for the given message.
		/// </summary>
		/// <param name="message">
		/// The message this state is related to.
		/// </param>
		/// <param name="status">
		/// The status of the message.
		/// </param>
		/// <param name="error">
		/// An optional error that occurred during the sending operation.
		/// </param>
		/// <param name="id">
		/// An optional identifier of the message state (when not provided,
		/// a new unique identifier is generated).
		/// </param>
		/// <param name="timeStamp">
		/// The optional time-stamp of the message state (when not
		/// provided, it defaults to the current UTC time).
		/// </param>
		/// <exception cref="ArgumentNullException">
		/// Thrown when the given <paramref name="message"/> is <c>null</c>.
		/// </exception>
		public LocalMessageState(IMessage message, MessageStatus status, IMessageError? error = null, string? id = null, DateTimeOffset? timeStamp = null) {
			Message = message;
			Status = status;
			Error = error;
			TimeStamp = timeStamp ?? DateTimeOffset.UtcNow;
			Id = id ?? Guid.NewGuid().ToString();
			Properties = message.Context != null
				? new Dictionary<string, object>(message.Context)
				: new Dictionary<string, object>();
		}

		/// <summary>
		/// Gets the message this state is related to.
		/// </summary>
		public IMessage Message { get; }

		/// <inheritdoc/>
		public string Id { get; }

		/// <inheritdoc/>
		public string MessageId => Message.Id;

		/// <inheritdoc/>
		public string? TenantId => Message.TenantId;

		/// <inheritdoc/>
		public MessageStatus Status { get; }

		/// <inheritdoc/>
		public IMessageError? Error { get; }

		IMessageError? IMessageState.RemoteError => null;

		/// <inheritdoc/>
		public DateTimeOffset TimeStamp { get; }

		/// <inheritdoc/>
		public IDictionary<string, object> Properties { get; set; }

		/// <summary>
		/// Creates a new instance of a <see cref="LocalMessageState"/> that
		/// indicates that the message was successfully sent.
		/// </summary>
		/// <param name="message">
		/// The message that was sent.
		/// </param>
		/// <param name="id">
		/// An optional identifier of the message state (when not provided,
		/// a new unique identifier is generated).
		/// </param>
		/// <param name="timeStamp">
		/// An optional time-stamp of the message state (when not provided,
		/// it defaults to the current UTC time).
		/// </param>
		/// <returns>
		/// Returns an instance of <see cref="LocalMessageState"/> that
		/// indicates that the message was successfully sent.
		/// </returns>
		public static LocalMessageState Sent(IMessage message, string? id = null, DateTimeOffset? timeStamp = null) {
			return new LocalMessageState(message, MessageStatus.Sent, id: id, timeStamp: timeStamp);
		}

		/// <summary>
		/// Creates a new instance of a <see cref="LocalMessageState"/> that
		/// represents a message that was failed to be sent.
		/// </summary>
		/// <param name="message">
		/// The message that was failed to be sent.
		/// </param>
		/// <param name="error">
		/// The instance of <see cref="IMessageError"/> that describes the
		/// error that occurred during the sending operation.
		/// </param>
		/// <param name="id">
		/// The optional identifier of the message state (when not provided,
		/// a new unique identifier is generated).
		/// </param>
		/// <param name="timeStamp">
		/// The optional time-stamp of the message state (when not provided,
		/// it defaults to the current UTC time).
		/// </param>
		/// <returns>
		/// Returns an instance of <see cref="LocalMessageState"/> that
		/// indicates that the message was failed to be sent.
		/// </returns>
		public static LocalMessageState DeliveryFailed(IMessage message, IMessageError? error = null, string? id = null, DateTimeOffset? timeStamp = null) {
			return new LocalMessageState(message, MessageStatus.DeliveryFailed, error, id, timeStamp);
		}
	}
}
