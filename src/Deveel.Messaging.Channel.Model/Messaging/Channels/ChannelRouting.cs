using System.Runtime.Serialization;

namespace Deveel.Messaging.Channels {
	/// <summary>
	/// The routing directions supported 
	/// by a channel.
	/// </summary>
	[Flags]
	public enum ChannelDirection {
		/// <summary>
		/// The channel doesn't support any routing.
		/// </summary>
		[EnumMember(Value = "none")]
		None = 0,

		/// <summary>
		/// The channel supports inbound routing
		/// of messages from queues.
		/// </summary>
		Inbound = 1,

		/// <summary>
		/// The channel supports outbound routing
		/// of messages to queues.
		/// </summary>
		Outbound = 2,

		/// <summary>
		/// The channel supports both inbound and
		/// outbound routing.
		/// </summary>
		Duplex = Inbound | Outbound
	}
}
