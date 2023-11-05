using System.Text.Json.Serialization;

namespace Deveel.Messaging.Channels {
	/// <summary>
	/// A default implementation of a messaging channel that is used
	/// to provide configurations and options to the connection to a
	/// provider of messaging services.
	/// </summary>
    public sealed class Channel : IChannel {
		/// <summary>
		/// Constructs the channel instance.
		/// </summary>
		public Channel() {
		}

		/// <summary>
		/// Constructs the channel instance from the given
		/// source channel.
		/// </summary>
		/// <param name="channel">
		/// The source channel to copy the properties from.
		/// </param>
		/// <exception cref="ArgumentNullException">
		/// Thrown if the given <paramref name="channel"/> is <c>null</c>.
		/// </exception>
		public Channel(IChannel channel) {
			ArgumentNullException.ThrowIfNull(channel, nameof(channel));

			Id = channel.Id;
			TenantId = channel.TenantId;
			Type = channel.Type;
			Provider = channel.Provider;
			Name = channel.Name;
			Directions = channel.Directions;
			Terminals = channel.Terminals?.Select(x => new ChannelTerminal(x)).ToList();
			ContentTypes = channel.ContentTypes?.ToArray() ?? Array.Empty<string>();
			Context = channel.Context?.ToDictionary(x => x.Key, y => y.Value);
			Options = channel.Options?.ToDictionary(x => x.Key, y => y.Value);
			Credentials = channel.Credentials?.Select(ChannelCredentials.From).ToList();
			Status = channel.Status;
		}

		/// <inheritdoc/>
		public string Type { get; set; }

		/// <inheritdoc/>
		public string Provider { get; set; }

		/// <inheritdoc/>
		public string Name { get; set; }

		/// <inheritdoc/>
		public ChannelDirection Directions { get; set; }

		IEnumerable<IChannelTerminal>? IChannel.Terminals => Terminals;

		public IList<ChannelTerminal>? Terminals { get; set; }

		IEnumerable<string> IChannel.ContentTypes => ContentTypes;

		public IList<string> ContentTypes { get; set;}

		/// <inheritdoc/>
		public IDictionary<string, object>? Context { get; set; }

		/// <inheritdoc/>
		public IDictionary<string, object>? Options { get; set; }

		IEnumerable<IChannelCredentials> IChannel.Credentials => Credentials;

		public IList<ChannelCredentials>? Credentials { get; set; }

		/// <inheritdoc/>
		public ChannelStatus Status { get; set; }

		/// <inheritdoc/>
		public string Id { get; set; }

		/// <inheritdoc/>
		public string? TenantId { get; set; }
	}
}
