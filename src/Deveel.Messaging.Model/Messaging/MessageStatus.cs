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

using System.Runtime.Serialization;

namespace Deveel.Messaging {
	/// <summary>
	/// Enumerates the possible status of a message
	/// at a given time in its handling lifecycle
	/// </summary>
	public enum MessageStatus {
		/// <summary>
		/// The status is unknown. Note: messages should
		/// never be in this status, but it is provided
		/// for completeness.
		/// </summary>
		[EnumMember(Value = "unknown")]
		Unknown = 0,

		/// <summary>
		/// The message has been received by the system.
		/// </summary>
		[EnumMember(Value = "received")]
		Received = 1,

		/// <summary>
		/// The message has been queued for delivery.
		/// </summary>
		[EnumMember(Value = "queued")]
		Queued = 2,

		/// <summary>
		/// The message was routed to the next node
		/// of the delivery chain.
		/// </summary>
		[EnumMember(Value = "routed")]
		Routed = 3,

		/// <summary>
		/// The message has been sent through the channel.
		/// </summary>
		[EnumMember(Value = "sent")]
		Sent = 4,

		/// <summary>
		/// The message has been delivered to the recipient.
		/// </summary>
		[EnumMember(Value = "delivered")]
		Delivered = 5,

		/// <summary>
		/// The message has failed to be delivered to the recipient.
		/// </summary>
		[EnumMember(Value = "deliveryFailed")]
		DeliveryFailed = 6,

		/// <summary>
		/// The message has been read by the recipient.
		/// </summary>
		[EnumMember(Value = "read")]
		Read = 7,

		/// <summary>
		/// The message has been deleted by the recipient.
		/// </summary>
		[EnumMember(Value = "deleted")]
		Deleted = 8
	}
}
