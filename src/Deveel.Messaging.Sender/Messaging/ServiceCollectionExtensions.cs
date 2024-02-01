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

using Microsoft.Extensions.DependencyInjection;

namespace Deveel.Messaging {
	/// <summary>
	/// Extends the <see cref="IServiceCollection"/> with methods
	/// to register a <see cref="MessageSender"/> in a service.
	/// </summary>
	public static class ServiceCollectionExtensions {
		/// <summary>
		/// Adds a <see cref="MessageSender"/> to the given collection
		/// of services.
		/// </summary>
		/// <param name="services">
		/// The collection of the services in which the sender will 
		/// be registered.
		/// </param>
		/// <returns>
		/// Returns the instance of the <see cref="MessageSenderBuilder"/>
		/// for further configuration.
		/// </returns>
		public static MessageSenderBuilder AddSender(this IServiceCollection services) {
			var builder = new MessageSenderBuilder(services);

			services.AddSingleton(builder);

			return builder;
		}

		/// <summary>
		/// Adds a <see cref="MessageSender"/> to the given collection
		/// of services, configuring it with the given action.
		/// </summary>
		/// <param name="services">
		/// The collection of the services in which the sender will
		/// be registered.
		/// </param>
		/// <param name="configure">
		/// The action to configure the sender.
		/// </param>
		/// <returns>
		/// Returns the collection of services for further registrations.
		/// </returns>
		public static IServiceCollection AddSender(this IServiceCollection services, Action<MessageSenderBuilder> configure) {
			var builder = services.AddSender();
			configure?.Invoke(builder);

			return services;
		}
	}
}
