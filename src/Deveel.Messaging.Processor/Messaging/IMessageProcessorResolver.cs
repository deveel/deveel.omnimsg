namespace Deveel.Messaging {
	/// <summary>
	/// A service that provides a mechanism to resolve
	/// instances of the message processors from a context.
	/// </summary>
	public interface IMessageProcessorResolver {
		/// <summary>
		/// Attempts to resolve a processor by the given name and version.
		/// </summary>
		/// <param name="name">
		/// The name of the processor to resolve.
		/// </param>
		/// <param name="version">
		/// The optional version of the processor to resolve.
		/// When not specified, the latest version is resolved.
		/// </param>
		/// <returns>
		/// Returns an instance of the <see cref="IMessageProcessor"/>
		/// that matches the given name and version, or <c>null</c>
		/// if no processor is found.
		/// </returns>
		IMessageProcessor? Resolve(string name, string? version = null);
	}
}
