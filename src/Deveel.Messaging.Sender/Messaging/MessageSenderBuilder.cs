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
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Deveel.Messaging {
	/// <summary>
	/// A builder for configuring a <see cref="MessageSender"/> instance
	/// to be used in a messaging service.
	/// </summary>
	public sealed class MessageSenderBuilder {
		internal MessageSenderBuilder(IServiceCollection services) {
			Services = services ?? throw new ArgumentNullException(nameof(services));

			AddDefaults();
		}

		/// <summary>
		/// Gets the collection of services used by the application,
		/// and where the builder will register the sender and its
		/// dependencies.
		/// </summary>
		public IServiceCollection Services { get; }

		private void AddDefaults() {
			Services.AddScoped<MessageSender>();
			// TODO: Services.AddScoped<MessageStateHandler>();
		}

		/// <summary>
		/// Registers a custom <see cref="MessageSender"/> implementation
		/// to be used in the messaging service.
		/// </summary>
		/// <param name="senderType">
		/// The type of the <see cref="MessageSender"/> to register.
		/// </param>
		/// <param name="lifetime">
		/// The lifetime of the registered service.
		/// </param>
		/// <returns>
		/// Returns this instance of the builder to allow 
		/// chaining calls.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// Thrown when the given <paramref name="senderType"/> is <c>null</c>.
		/// </exception>
		/// <exception cref="ArgumentException">
		/// Thrown when the given <paramref name="senderType"/> is not a valid
		/// and not assignable to <see cref="MessageSender"/>.
		/// </exception>
		public MessageSenderBuilder UseSender(Type senderType, ServiceLifetime lifetime = ServiceLifetime.Scoped) {
			if (senderType == null)
				throw new ArgumentNullException(nameof(senderType));
			if (!typeof(MessageSender).IsAssignableFrom(senderType))
				throw new ArgumentException($"The type {senderType} is not a valid sender type.");

			Services.Add(new ServiceDescriptor(senderType, senderType, lifetime));

			if (senderType != typeof(MessageSender))
				Services.Add(new ServiceDescriptor(typeof(MessageSender), senderType, lifetime));

			return this;
		}

		/// <summary>
		/// Registers a custom <see cref="MessageSender"/> implementation
		/// to be used in the messaging service.
		/// </summary>
		/// <typeparam name="TSender">
		/// The type of the <see cref="MessageSender"/> to register.
		/// </typeparam>
		/// <param name="lifetime">
		/// The lifetime of the registered service.
		/// </param>
		/// <returns>
		/// Returns this instance of the builder to allow
		/// chaining calls.
		/// </returns>
		/// <seealso cref="UseSender(Type, ServiceLifetime)"/>
		public MessageSenderBuilder UseSender<TSender>(ServiceLifetime lifetime = ServiceLifetime.Scoped)
			where TSender : MessageSender
			=> UseSender(typeof(TSender), lifetime);

		/// <summary>
		/// Configures the options of the message sender service.
		/// </summary>
		/// <param name="configure">
		/// An action that is used to configure the options.
		/// </param>
		/// <returns>
		/// Returns this instance of the builder to allow
		/// chaining calls.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// Thrown when the given <paramref name="configure"/> action is <c>null</c>.
		/// </exception>
		public MessageSenderBuilder Configure(Action<MessageSenderOptions> configure) {
			ArgumentNullException.ThrowIfNull(configure);

			Services.AddOptions<MessageSenderOptions>()
				.Configure(configure);

			return this;
		}

		/// <summary>
		/// Configures the options of the message sender service
		/// using the given configuration section from
		/// the application configuration.
		/// </summary>
		/// <param name="sectionName">
		/// The name of the configuration section to use.
		/// </param>
		/// <returns>
		/// Returns this instance of the builder to allow
		/// chaining calls.
		/// </returns>
		/// <exception cref="ArgumentException">
		/// Thrown when the given <paramref name="sectionName"/> is <c>null</c> 
		/// or empty.
		/// </exception>
		public MessageSenderBuilder Configure(string sectionName) {
			if (string.IsNullOrWhiteSpace(sectionName))
				throw new ArgumentException($"'{nameof(sectionName)}' cannot be null or whitespace.", nameof(sectionName));

			Services.AddOptions<MessageSenderOptions>()
				.BindConfiguration(sectionName);

			return this;
		}
	}
}
