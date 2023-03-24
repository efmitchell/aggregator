using System.Runtime.ExceptionServices;
using Aggregator.Models;
using Aggregator.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Aggregator;

internal static class Program
{
    private static async Task Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((_, services) => { services.AddAggregatorServices(); })
            .Build();

        var priceEngine = host.Services.GetRequiredService<PriceEngine>();

        // Omitted for brevity - Collect input (risk data from the user)
        var request = new PriceRequest
        {
            // Hardcoded here, but would normally be created from the user input above
            RiskData = new RiskData
            {
                DOB = DateTime.Parse("1980-01-01"),
                FirstName = "John",
                LastName = "Smith",
                Make = "Cool New Phone",
                Value = 500
            }
        };

        try
        {
            var response = await priceEngine.GetPriceAsync(request);

            if (response.Price == -1)
                Console.Out.WriteLine($"There was an error - {response.Error}");
            else
                Console.Out.WriteLine(
                    $"Your price is {response.Price}, from insurer: {response.Insurer}. This includes tax of {response.Tax}");
        }
        catch (Exception ex)
        {
            ExceptionDispatchInfo.Capture(ex).Throw();
        }
    }
}