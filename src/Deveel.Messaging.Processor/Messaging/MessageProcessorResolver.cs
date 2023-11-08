namespace Deveel.Messaging {
	/// <summary>
	/// A default implementation of <see cref="IMessageProcessorResolver"/>
	/// that uses a collection of <see cref="ProcessorDescriptor"/> available
	/// in the context to resolve the processors.
	/// </summary>
	public class MessageProcessorResolver : IMessageProcessorResolver {
		private readonly IEnumerable<ProcessorDescriptor> descriptors;
		private readonly IServiceProvider serviceProvider;

		/// <summary>
		/// Constructs the resolver with the given service provider and
		/// a list of descriptors.
		/// </summary>
		/// <param name="serviceProvider"></param>
		/// <param name="descriptors"></param>
		public MessageProcessorResolver(IServiceProvider serviceProvider, IEnumerable<ProcessorDescriptor> descriptors) {
			this.serviceProvider = serviceProvider;
			this.descriptors = descriptors;
		}

		/// <summary>
		/// Resolves the descriptor of a processor by the given name and version.
		/// </summary>
		/// <param name="name">
		/// The name of the processor to resolve.
		/// </param>
		/// <param name="version">
		/// The optional version of the processor to resolve.
		/// </param>
		/// <returns>
		/// Returns an instance of the <see cref="ProcessorDescriptor"/> that
		/// is the metadata of the processor that matches the given name and
		/// version, or <c>null</c> if no processor is found.
		/// </returns>
		protected virtual ProcessorDescriptor? ResolveDescriptor(string name, string? version = null) {
			ArgumentNullException.ThrowIfNull(name, nameof(name));

			var descriptors = this.descriptors.Where(x => x.Name == name);
			if (!String.IsNullOrWhiteSpace(version)) {
				var versionNumber = Version.Parse(version);
				descriptors = descriptors.Where(x => x.Version == versionNumber);
			} else {
				descriptors = descriptors.OrderByDescending(x => x.Version);
			}

			return descriptors.FirstOrDefault();
		}

		/// <inheritdoc/>
		public IMessageProcessor? Resolve(string name, string? version = null) {
			var descriptor = ResolveDescriptor(name, version);

			if (descriptor == null)
				return null;

			return (IMessageProcessor?) serviceProvider.GetService(descriptor.ProcessorType);
		}
	}
}
