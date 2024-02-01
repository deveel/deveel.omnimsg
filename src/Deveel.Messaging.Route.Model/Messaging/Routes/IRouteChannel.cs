namespace Deveel.Messaging.Routes {
	/// <summary>
	/// Describes the channel of a route.
	/// </summary>
	public interface IRouteChannel {
		/// <summary>
		/// Gets the unique identifier of the channel.
		/// </summary>
		string Id { get; }

		/// <summary>
		/// Gets the name of the channel.
		/// </summary>
		string Name { get; }

		/// <summary>
		/// Gets the type of the channel.
		/// </summary>
		string Type { get; }

		/// <summary>
		/// Gets the provider of the messaging
		/// services for the channel.
		/// </summary>
		string Provider { get; }
	}
}
