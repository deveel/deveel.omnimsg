namespace Deveel.Messaging.Channels {
	/// <summary>
	/// Describes a constraining schema for the
	/// configuration of a messaging channel.
	/// </summary>
	public interface IChannelSchema {
		/// <summary>
		/// Gets the type of the channel.
		/// </summary>
		string Type { get; }

		/// <summary>
		/// Gets the identifier of the provider of the 
		/// messaging services.
		/// </summary>
		string Provider { get; }

		/// <summary>
		/// Gets the routing directions supported by the 
		/// channel.
		/// </summary>
		ChannelDirection Directions { get; }

		/// <summary>
		/// Gets the list of the types of terminals
		/// that are allowed to be used as senders
		/// of a message processed by the channel.
		/// </summary>
		IEnumerable<string> AllowedSenderTypes { get; }

		/// <summary>
		/// Gets the list of the types of terminals
		/// that are required to be used as senders 
		/// of a message processed by the channel.
		/// </summary>
		IEnumerable<string> RequiredSenderTypes { get; }

		/// <summary>
		/// Gets the list of the types of terminals
		/// that are allowed to be used as receivers
		/// of a message processed by the channel.
		/// </summary>
		IEnumerable<string> AllowedReceiverTypes { get; }

		/// <summary>
		/// Gets the list of the types of terminals
		/// that are required to be used as receivers
		/// of a message processed by the channel.
		/// </summary>
		IEnumerable<string> RequiredReceiverTypes { get; }

		/// <summary>
		/// Gets the list of the types of message content that
		/// are allowed to be transported by the channel.
		/// </summary>
		IEnumerable<string> AllowedContentTypes { get; }

		/// <summary>
		/// Gets the list of the required types of
		/// credentials that a channel instance must
		/// provide to be used.
		/// </summary>
		IEnumerable<string> CredentialTypes { get; }

		/// <summary>
		/// Gets the list of the option keys that are allowed
		/// to be used in the configuration of the channel.
		/// </summary>
		IEnumerable<string> Options { get; }
	}
}
