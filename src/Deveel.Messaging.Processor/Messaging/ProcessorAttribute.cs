namespace Deveel.Messaging {
	/// <summary>
	/// An attribute that provides metadata about a processor.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public sealed class ProcessorAttribute : Attribute {
		/// <summary>
		/// Constructs the attribute with the given name and version.
		/// </summary>
		/// <param name="name">
		/// The name of the processor.
		/// </param>
		/// <param name="version">
		/// The version of the processor.
		/// </param>
		public ProcessorAttribute(string name, string version) {
			ArgumentNullException.ThrowIfNull(name, nameof(name));
			ArgumentNullException.ThrowIfNull(version, nameof(version));

			Name = name;
			Version = version;
		}

		/// <summary>
		/// Gets or sets the name of the processor.
		/// </summary>
		public string Name { get; }

		/// <summary>
		/// Gets or sets the version of the processor.
		/// </summary>
		public string Version { get; }
	}
}
