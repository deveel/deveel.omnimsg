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
	/// <summary>
	/// Describes an error that is associated to a message.
	/// </summary>
	public interface IMessageError {
		/// <summary>
		/// Gets the error code that is assigned
		/// by the messaging system.
		/// </summary>
		string Code { get; }

		/// <summary>
		/// Gets a descriptive message of the error.
		/// </summary>
		string? Message { get; }

		/// <summary>
		/// Gets an optional error that is the cause
		/// of this error.
		/// </summary>
		/// <remarks>
		/// In a messaging system, this type of error is
		/// typically the error that is received from a remote
		/// message channel.
		/// </remarks>
		IMessageError? InnerError { get; }
	}
}
