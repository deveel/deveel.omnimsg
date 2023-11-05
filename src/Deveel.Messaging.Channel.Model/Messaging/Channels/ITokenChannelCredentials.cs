namespace Deveel.Messaging.Channels {
	/// <summary>
	/// Describes a static token that is used to authenticate
	/// a connection to a messaging channel.
	/// </summary>
	public interface ITokenChannelCredentials : IChannelCredentials {
		/// <summary>
		/// Gets the authentication scheme used by the token.
		/// </summary>
		string? Scheme { get; }

		/// <summary>
		/// Gets the token used to authenticate the connection.
		/// </summary>
		string Token { get; }
	}
}
