namespace Deveel.Messaging.Channels {
	/// <summary>
	/// Options for the resolution of a channel.
	/// </summary>
	public sealed class ChannelResolutionOptions {
		/// <summary>
		/// Gets or sets the identifier of the tenant
		/// that owns the channel to resolve.
		/// </summary>
		public string? TenantId { get; set; }

		/// <summary>
		/// Gets or sets a flag indicating if the credentials
		/// of the channel should be included in the resolution.
		/// </summary>
		public bool? IncludeCredentials { get; set; }
	}
}
