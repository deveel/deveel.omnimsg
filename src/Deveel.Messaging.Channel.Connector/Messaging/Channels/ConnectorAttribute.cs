namespace Deveel.Messaging.Channels {
	/// <summary>
	/// Provides a description of a channel connector metadata.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	public sealed class ConnectorAttribute : Attribute {
		/// <summary>
		/// Constructs the attribute with the given type and provider
		/// </summary>
		/// <param name="type">
		/// The type of the channel connector.
		/// </param>
		/// <param name="provider">
		/// The provider of the messaging services.
		/// </param>
		/// <exception cref="ArgumentNullException">
		/// Thrown when either <paramref name="type"/> or <paramref name="provider"/>
		/// are <c>null</c>.
		/// </exception>
		public ConnectorAttribute(string type, string provider) {
			Type = type ?? throw new ArgumentNullException(nameof(type));
			Provider = provider ?? throw new ArgumentNullException(nameof(provider));
		}

		/// <summary>
		/// Gets the type of the channel connector.
		/// </summary>
		public string Type { get; }

		/// <summary>
		/// Gets the identifier of the provider of the
		/// messaging services.
		/// </summary>
		public string Provider { get; }
	}
}
