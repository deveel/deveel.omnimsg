namespace Deveel.Messaging {
	/// <summary>
	/// A descriptor that provides metadata about a message 
	/// processor service.
	/// </summary>
	public sealed class ProcessorDescriptor {
		/// <summary>
		/// Constructs the descriptor with the given name, version
		/// and the type of the processor.
		/// </summary>
		/// <param name="name">
		/// The name of the processor.
		/// </param>
		/// <param name="version">
		/// The version of the processor.
		/// </param>
		/// <param name="processorType">
		/// The type of the message processor.
		/// </param>
		/// <exception cref="ArgumentNullException">
		/// Thrown when the given <paramref name="name"/>, <paramref name="version"/> 
		/// or <paramref name="processorType"/> are <c>null</c>.
		/// </exception>
		/// <exception cref="ArgumentException">
		/// Thrown when the given <paramref name="processorType"/> is not
		/// an implementation of <see cref="IMessageProcessor"/>.
		/// </exception>
		public ProcessorDescriptor(string name, Version version, Type processorType) {
			ArgumentNullException.ThrowIfNull(name, nameof(name));
			ArgumentNullException.ThrowIfNull(processorType, nameof(processorType));
			ArgumentNullException.ThrowIfNull(version, nameof(version));

			if (!typeof(IMessageProcessor).IsAssignableFrom(processorType))
				throw new ArgumentException($"The type {processorType} is not a valid processor");

			Name = name;
			Version = version;
			ProcessorType = processorType;
		}

		/// <summary>
		/// Gets the name of the processor.
		/// </summary>
		public string Name { get; }

		/// <summary>
		/// Gets the version of the processor.
		/// </summary>
		public Version Version { get; }

		/// <summary>
		/// Gets the type of the processor.
		/// </summary>
		public Type ProcessorType { get; }
	}
}
