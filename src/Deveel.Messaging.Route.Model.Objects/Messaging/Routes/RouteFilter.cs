namespace Deveel.Messaging.Routes {
	/// <summary>
	/// A base implementation of a <see cref="IRouteFilter"/>.
	/// </summary>
	public abstract class RouteFilter : IRouteFilter {
		/// <summary>
		/// Gets the type of the filter.
		/// </summary>
		/// <seealso cref="IRouteFilter.FilterType"/>
		protected abstract string FilterType { get; }

		string IRouteFilter.FilterType => FilterType;

		/// <summary>
		/// A factory method that creates an instance of <see cref="RouteFilter"/>
		/// from the given <paramref name="filter"/>.
		/// </summary>
		/// <param name="filter">
		/// The source filter to create the instance from.
		/// </param>
		/// <returns>
		/// Returns an instance of <see cref="RouteFilter"/> that is created
		/// from the given <paramref name="filter"/>.
		/// </returns>
		/// <exception cref="NotSupportedException">
		/// Thrown when the given <paramref name="filter"/> is not supported.
		/// </exception>
		public static RouteFilter Create(IRouteFilter filter) {
			if (filter is IEmptyRouteFilter empty && empty.FilterType == KnownRouteFilterTypes.Empty)
				return new EmptyRouteFilter();

			if (filter is IExpressionRouteFilter expression && expression.FilterType == KnownRouteFilterTypes.Expression)
				return new ExpressionRouteFilter(expression);

			throw new NotSupportedException($"The filter type {filter.FilterType} is not supported.");
		}
	}
}
