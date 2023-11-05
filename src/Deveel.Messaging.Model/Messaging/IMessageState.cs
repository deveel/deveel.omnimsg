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
	/// Describes the status of a message at a given time
	/// in its life-cycle
	/// </summary>
	/// <remarks>
	/// The nature of messaging operations is asynchronous and
	/// therefore a message can be in different states during
	/// its life-cycle: this entity describes the state of the
	/// message at a given time, without carrying the whole
	/// information itself.
	/// </remarks>
	public interface IMessageState {
		/// <summary>
		/// Gets the unique identifier of the state
		/// </summary>
		string Id { get; }

		/// <summary>
		/// Gets the unique identifier of the tenant
		/// that is the owner of the message
		/// </summary>
		string? TenantId { get; }

		/// <summary>
		/// Gets a reference to the message that is
		/// the subject of the state
		/// </summary>
		string MessageId { get; }

		/// <summary>
		/// Gets the status of the message in the state
		/// </summary>
		MessageStatus Status { get; }

		/// <summary>
		/// When the message is in an error state, this
		/// returns the error that is associated to the
		/// message by the messaging system.
		/// </summary>
		IMessageError? Error { get; }

		/// <summary>
		/// When an error occurred in the remote provider,
		/// this returns the instance of the error.
		/// </summary>
		IMessageError? RemoteError { get; }

		/// <summary>
		/// Gets the exact time when the message state
		/// was created
		/// </summary>
		DateTimeOffset TimeStamp { get; }

		/// <summary>
		/// Gets a dictionary of properties that are
		/// describing the state of the message
		/// </summary>
		IDictionary<string, object>? Properties { get; }
	}
}
