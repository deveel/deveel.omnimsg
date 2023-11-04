namespace Deveel.Messaging {
	/// <summary>
	/// Provides a contract for entities that are able to
	/// provide additional context to a message.
	/// </summary>
	public interface IMessageContextProvider {
		/// <summary>
		/// Gets the context provided by an entity
		/// to a message.
		/// </summary>
		IDictionary<string, object> Context { get; }
	}
}
