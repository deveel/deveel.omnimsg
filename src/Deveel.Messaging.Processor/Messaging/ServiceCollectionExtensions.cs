using System.Reflection;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Deveel.Messaging {
	/// <summary>
	/// Extends the <see cref="IServiceCollection"/> to provide methods
	/// for registering a <see cref="IMessageProcessor"/> in the service
	/// collection of the application.
	/// </summary>
	public static class ServiceCollectionExtensions {
		// TODO: should we use Scrutor instead?

		/// <summary>
		/// Registers a processor in the service collection.
		/// </summary>
		/// <param name="services">
		/// The service collection to register the processor.
		/// </param>
		/// <param name="processorType">
		/// The type of the processor to register.
		/// </param>
		/// <param name="lifetime">
		/// The desired lifetime of the processor.
		/// </param>
		/// <remarks>
		/// This method will attempt to find the metadata of the processor
		/// using the <see cref="ProcessorAttribute"/> attribute.
		/// </remarks>
		/// <returns>
		/// Returns the same <see cref="IServiceCollection"/> to allow
		/// chaining of calls.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// Thrown when the given <paramref name="processorType"/> is <c>null</c>.
		/// </exception>
		/// <exception cref="ArgumentException">
		/// Thrown when the given <paramref name="processorType"/> is not
		/// an implementation of <see cref="IMessageProcessor"/>, or when
		/// the processor has no name.
		/// </exception>
		public static IServiceCollection AddProcessor(this IServiceCollection services, Type processorType, ServiceLifetime lifetime = ServiceLifetime.Scoped) {
			ArgumentNullException.ThrowIfNull(processorType, nameof(processorType));

			if (!typeof(IMessageProcessor).IsAssignableFrom(processorType))
				throw new ArgumentException($"The type {processorType} is not a valid processor");

			var attribute = processorType.GetCustomAttribute<ProcessorAttribute>();
			if (attribute != null) {
				var name = attribute.Name;
				if (String.IsNullOrWhiteSpace(name))
					throw new ArgumentException($"The processor {processorType} has no name");

				Version? version = null;
				if (!String.IsNullOrWhiteSpace(attribute.Version)) {
					if (!Version.TryParse(attribute.Version, out version))
						throw new ArgumentException($"The processor {processorType} has an invalid version {attribute.Version}");
				}

				if (version == null)
					throw new ArgumentException($"The processor {processorType} has no valid version");

				services.AddSingleton(new ProcessorDescriptor(name, version, processorType));
			}

			services.TryAddEnumerable(ServiceDescriptor.Describe(typeof(IMessageProcessor), processorType, lifetime));
			services.TryAdd(ServiceDescriptor.Describe(processorType, processorType, lifetime));

			services.TryAddScoped<IMessageProcessorResolver, MessageProcessorResolver>();

			return services;
		}

		/// <summary>
		/// Registers a processor in the service collection.
		/// </summary>
		/// <typeparam name="TProcesor"></typeparam>
		/// <param name="services"></param>
		/// <param name="lifetime"></param>
		/// <returns></returns>
		public static IServiceCollection AddProcessor<TProcesor>(this IServiceCollection services, ServiceLifetime lifetime = ServiceLifetime.Scoped) where TProcesor : IMessageProcessor
			=> services.AddProcessor(typeof(TProcesor), lifetime);
	}
}
