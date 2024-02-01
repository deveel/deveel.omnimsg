namespace Deveel.Messaging.Routes {
	/// <summary>
	/// A route filter that is used to filter messages based on an expression.
	/// </summary>
	public class ExpressionRouteFilter : RouteFilter, IExpressionRouteFilter {
		/// <summary>
		/// Constucts an empty expression filter.
		/// </summary>
		public ExpressionRouteFilter() {
		}

		/// <summary>
		/// Constructs an expression filter from the given <paramref name="filter"/>.
		/// </summary>
		/// <param name="filter">
		/// The source filter to copy the expression from.
		/// </param>
		public ExpressionRouteFilter(IExpressionRouteFilter filter) {
			Expression = filter.Expression;
			Language = filter.Language;
		}

		/// <summary>
		/// Constructs an expression filter with the given <paramref name="expression"/>
		/// of the given <paramref name="language"/>.
		/// </summary>
		/// <param name="expression">
		/// The expression string to use to filter a route from the message.
		/// </param>
		/// <param name="language">
		/// The language used to parse the expression.
		/// </param>
		public ExpressionRouteFilter(string expression, string? language = null) {
			Expression = expression;
			Language = language;
		}

		/// <inheritdoc/>
		protected override string FilterType => KnownRouteFilterTypes.Expression;

		/// <inheritdoc/>
		public string Expression { get; set; }

		/// <inheritdoc/>
		public string? Language { get; set; }
	}
}
