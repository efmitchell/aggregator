using Aggregator.Exceptions;
using Aggregator.Models;
using Aggregator.QuotationSystem;

namespace Aggregator;

public class PriceEngine
{
    private readonly IEnumerable<IQuotationSystem> _quotationSystems;

    //Pass request with risk data with details of a gadget, return the best price retrieved from 3 external quotation engines
    public PriceEngine(IEnumerable<IQuotationSystem> quotationSystems)
    {
        _quotationSystems = quotationSystems;
    }

    public async Task<PriceResponse> GetPriceAsync(PriceRequest request)
    {
        try
        {
            request.RiskData.Validate();
        }
        catch (ValidationException ex)
        {
            return new PriceResponse { Error = ex.Message, Price = -1 };
        }

        var result = await FindBestPriceAsync(request.RiskData);

        if (!string.IsNullOrEmpty(result.Error)) throw new Exception(result.Error);

        return result;
    }

    private async Task<PriceResponse> FindBestPriceAsync(RiskData riskData)
    {
        if (riskData == null) throw new ArgumentNullException(nameof(riskData));

        PriceResponse bestPriceResponse = null;

        foreach (var quotationSystem in _quotationSystems)
        {
            var priceResponse = await quotationSystem.GetPriceAsync(riskData);

            if (priceResponse.Error == null &&
                (bestPriceResponse == null || priceResponse.Price < bestPriceResponse.Price))
                bestPriceResponse = priceResponse;
        }

        return bestPriceResponse ?? new PriceResponse { Error = "No valid quotes found", Price = -1 };
    }
}