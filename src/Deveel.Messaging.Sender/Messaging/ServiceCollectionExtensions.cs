using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;

namespace Deveel.Messaging {
	public static class ServiceCollectionExtensions {
		public static MessageSenderBuilder AddSender(this IServiceCollection services) {
			var builder = new MessageSenderBuilder(services);

			services.AddSingleton(builder);

			return builder;
		}

		public static IServiceCollection AddSender(this IServiceCollection services, Action<MessageSenderBuilder> configure) {
			var builder = services.AddSender();
			configure?.Invoke(builder);

			return services;
		}
	}
}
