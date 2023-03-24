using Aggregator.QuotationSystem;
using Aggregator.Services;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Aggregator.Tests
{
    public class ServiceCollectionExtensionsTests
    {
        [Fact]
        public void AddAggregatorServices_RegistersQuotationSystemsAndPriceEngine()
        {
            // Arrange
            var services = new ServiceCollection();

            // Act
            services.AddAggregatorServices();
            var serviceProvider = services.BuildServiceProvider();

            // Assert
            var quotationSystems = serviceProvider.GetServices<IQuotationSystem>().ToList();
            Assert.Equal(3, quotationSystems.Count);
            Assert.IsType<QuotationSystem1>(quotationSystems[0]);
            Assert.IsType<QuotationSystem2>(quotationSystems[1]);
            Assert.IsType<QuotationSystem3>(quotationSystems[2]);

            var priceEngine = serviceProvider.GetService<PriceEngine>();
            Assert.NotNull(priceEngine);
        }
    }
}