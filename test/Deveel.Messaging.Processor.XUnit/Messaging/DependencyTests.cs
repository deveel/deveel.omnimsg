using Microsoft.Extensions.DependencyInjection;

namespace Deveel.Messaging {
	public static class DependencyTests {
		[Fact]
		public static void ResolveProcessorByType() {
			var services = new ServiceCollection();

			services.AddProcessor<TestProcessor>();

			var provider = services.BuildServiceProvider();

			var processor = provider.GetService<IMessageProcessor>();

			Assert.NotNull(processor);
			Assert.IsType<TestProcessor>(processor);
		}

		[Theory]
		[InlineData("test", true)]
		[InlineData("test2", false)]
		public static void ResolveProcessorByName(string name, bool found) {
			var services = new ServiceCollection();

			services.AddProcessor<TestProcessor>();

			var provider = services.BuildServiceProvider();

			var resolver = provider.GetService<IMessageProcessorResolver>();

			Assert.NotNull(resolver);
			Assert.IsType<MessageProcessorResolver>(resolver);

			var processor = resolver.Resolve(name);

			Assert.Equal(found, processor != null);
		}

		[Theory]
		[InlineData("test", "1.0", true)]
		[InlineData("test", "2.0", false)]
		[InlineData("test2", "1.0", false)]
		public static void ResolveProcessorByNameAndVersion(string name, string version, bool found) {
			var services = new ServiceCollection();

			services.AddProcessor<TestProcessor>();

			var provider = services.BuildServiceProvider();

			var resolver = provider.GetService<IMessageProcessorResolver>();

			Assert.NotNull(resolver);
			Assert.IsType<MessageProcessorResolver>(resolver);

			var processor = resolver.Resolve(name, version);

			Assert.Equal(found,  processor != null);
		}
	}

	[Processor("test", "1.0")]
	class TestProcessor : IMessageProcessor {
		public Task<MessageProcessResult> ProcessAsync(IMessage message, CancellationToken cancellationToken = default) {
			return Task.FromResult(MessageProcessResult.Success(message));
		}
	}
}
