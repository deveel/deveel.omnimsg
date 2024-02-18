namespace Deveel.Messaging {
	public static class MessageStateExtensions {
		public static bool IsDelivered(this IMessageState state)
			=> state.Status == MessageStatus.Delivered;

		public static bool IsDeliveryFailed(this IMessageState state)
			=> state.Status == MessageStatus.DeliveryFailed;

		public static bool IsSent(this IMessageState state)
			=> state.Status == MessageStatus.Sent;

		public static bool IsReceived(this IMessageState state)
			=> state.Status == MessageStatus.Received;

	}
}
