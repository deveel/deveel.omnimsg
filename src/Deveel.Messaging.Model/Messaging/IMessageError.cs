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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
		/// Gets a flag indicating if the error occurred
		/// in the local messaging system or in the remote
		/// provider of the messaging services.
		/// </summary>
		bool IsRemote { get; }
	}
}
