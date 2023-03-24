using Aggregator.QuotationSystem;
using Microsoft.Extensions.DependencyInjection;

namespace Aggregator.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAggregatorServices(this IServiceCollection services)
        {
            // Register your quotation systems here
            services.AddSingleton<IQuotationSystem>(new QuotationSystem1("http://quote-system-1.com", "1234"));
            services.AddSingleton<IQuotationSystem>(new QuotationSystem2("http://quote-system-2.com", "1235"));
            services.AddSingleton<IQuotationSystem>(new QuotationSystem3("http://quote-system-3.com", "100"));

            // Register the price engine
            services.AddTransient<PriceEngine>();

            return services;
        }
    }
}