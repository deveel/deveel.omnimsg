namespace Deveel.Messaging.Channels {
	/// <summary>
	/// A pair of username and password used to authenticate
	/// the connection to a channel.
	/// </summary>
	public interface IBasicAuthChannelCredentials : IChannelCredentials {
		/// <summary>
		/// Gets the username used to authenticate the connection.
		/// </summary>
		string Username { get; }

		/// <summary>
		/// Gets the secret password that is used to authenticate
		/// the connection.
		/// </summary>
		string Password { get; }
	}
}
