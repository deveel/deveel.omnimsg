namespace Deveel.Messaging.Channels {
	/// <summary>
	/// A client service that is able to resolve the schema
	/// for a channel.
	/// </summary>
	public interface IChannelSchemaResolver {
		/// <summary>
		/// Resolves the schema for a channel of the given type
		/// and provider.
		/// </summary>
		/// <param name="type">
		/// The type of the channel to resolve.
		/// </param>
		/// <param name="provider">
		/// The identifier of the provider of the channel.
		/// </param>
		/// <returns>
		/// Returns an instance of <see cref="IChannelSchema"/> that
		/// describes the schema of the channel.
		/// </returns>
		IChannelSchema? Resolve(string type, string provider);
	}
}
