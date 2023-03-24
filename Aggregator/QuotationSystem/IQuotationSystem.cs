namespace Aggregator.QuotationSystem;

public interface IQuotationSystem
{
    Task<dynamic> GetPriceAsync(RiskData riskData);
}

public abstract class QuotationSystemBase : IQuotationSystem
{
    protected string ApiUrl { get; }

    protected QuotationSystemBase(string apiUrl)
    {
        ApiUrl = apiUrl;
    }

    public abstract Task<dynamic> GetPriceAsync(RiskData riskData);
}