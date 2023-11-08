using System.Reflection;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Deveel.Messaging.Channels {
	public static class ServiceCollectionExtensions {
		public static IServiceCollection AddChannelConnector(this IServiceCollection services, Type processorType, ServiceLifetime lifetime = ServiceLifetime.Scoped) {
			ArgumentNullException.ThrowIfNull(processorType, nameof(processorType));

			if (!typeof(IChannelConnector).IsAssignableFrom(processorType))
				throw new ArgumentException($"The type {processorType} is not a valid processor");

			var attribute = processorType.GetCustomAttribute<ConnectorAttribute>();
			if (attribute != null) {
				services.AddSingleton(new ConnectorDescriptor(attribute.Type, attribute.Provider, processorType));
			}

			services.TryAddEnumerable(ServiceDescriptor.Describe(typeof(IChannelConnector), processorType, lifetime));
			services.TryAdd(ServiceDescriptor.Describe(processorType, processorType, lifetime));

			services.TryAddScoped<IChannelConnectorResolver, ChannelConnectorResolver>();

			return services;
		}

		public static IServiceCollection AddChannelConnector<TConnector>(this IServiceCollection services, ServiceLifetime lifetime = ServiceLifetime.Scoped)
			where TConnector : class, IChannelConnector {
			return services.AddChannelConnector(typeof(TConnector), lifetime);
		}
	}
}
