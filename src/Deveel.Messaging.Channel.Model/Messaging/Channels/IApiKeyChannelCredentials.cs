namespace Deveel.Messaging.Channels {
	/// <summary>
	/// A type of credentials that represents an API key.
	/// </summary>
	/// <remarks>
	/// The location of the API key is provider-specific:
	/// it is up to the implementation of the provider to
	/// use the key in the proper way.
	/// </remarks>
	public interface IApiKeyChannelCredentials : IChannelCredentials {
		/// <summary>
		/// Gets the API key used to authenticate the channel.
		/// </summary>
		string ApiKey { get; }
	}
}
