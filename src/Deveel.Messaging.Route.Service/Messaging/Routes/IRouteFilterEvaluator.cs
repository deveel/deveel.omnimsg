namespace Deveel.Messaging.Routes {
	/// <summary>
	/// A service that is used to evaluate filters
	/// of a certain format.
	/// </summary>
	public interface IRouteFilterEvaluator {
		/// <summary>
		/// Evaluate the given filter against the provided message
		/// argument.
		/// </summary>
		/// <param name="filter">
		/// The instance of the filter to evaluate.
		/// </param>
		/// <param name="message">
		/// The message argument of the evaluation.
		/// </param>
		/// <param name="cancellationToken">
		/// A token used to cancel the evaluation.
		/// </param>
		/// <returns>
		/// Returns a boolean value indicating the result of
		/// the filter evaluation.
		/// </returns>
		Task<bool> EvaluateAsync(IMessage message, IRouteFilter filter, CancellationToken cancellationToken = default);
	}
}
