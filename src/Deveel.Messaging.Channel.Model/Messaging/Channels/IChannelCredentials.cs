namespace Deveel.Messaging.Channels {
	/// <summary>
	/// The contract of a set of credentials that are used to
	/// authenticate a channel to a messaging service.
	/// </summary>
	public interface IChannelCredentials {
		/// <summary>
		/// Gets the type of credentials.
		/// </summary>
		string Type { get; }
	}
}
