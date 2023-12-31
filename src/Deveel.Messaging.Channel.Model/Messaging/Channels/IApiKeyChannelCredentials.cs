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

namespace Deveel.Messaging.Channels {
	/// <summary>
	/// A type of credentials that represents an API key.
	/// </summary>
	/// <remarks>
	/// The location of the API key is provider-specific:
	/// it is up to the implementation of the provider to
	/// use the key in the proper way.
	/// </remarks>
	public interface IApiKeyChannelCredentials : IChannelCredentials {
		/// <summary>
		/// Gets the API key used to authenticate the channel.
		/// </summary>
		string ApiKey { get; }
	}
}
