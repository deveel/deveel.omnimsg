namespace Deveel.Messaging.Routes {
	/// <summary>
	/// The reference to a channel associated to a route.
	/// </summary>
	public class RouteChannel : IRouteChannel {
		public RouteChannel() {
		}

		public RouteChannel(IRouteChannel channel) {
			Id = channel.Id;
			Name = channel.Name;
			Type = channel.Type;
			Provider = channel.Provider;
		}

		/// <inheritdoc/>
		public string Id { get; set; }

		/// <inheritdoc/>
		public string Name { get; set; }

		/// <inheritdoc/>
		public string Type { get; set; }

		/// <inheritdoc/>
		public string Provider { get; set; }
	}
}
