
using System.Collections.Concurrent;

namespace Deveel.Messaging.Routes {
	class TestRouteRetriever : IRouteRetriever {
		private readonly List<IMessageRoute> routes = new List<IMessageRoute>();

		public Task<IList<IMessageRoute>> ListRoutesAsync(RouteRetrievalOptions? options = null, CancellationToken cancellationToken = default) => throw new NotImplementedException();
	}
}
