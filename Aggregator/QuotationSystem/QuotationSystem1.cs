using System.Dynamic;

namespace Aggregator.QuotationSystem;

public class QuotationSystem1 : IQuotationSystem
{
    public QuotationSystem1(string url, string port)
    {
        
    }

    public Task<dynamic> GetPriceAsync(RiskData riskData) // Not async but would would update this to be if call from external service
    {
        //Omitted - Call to an external service
        //var response = _someExternalService.PostHttpRequest(requestData);
        // Perhaps worth considering caching to reduce requests to quotation system? 
        // May be worth implementing a backoff or circuit breaker? 

        dynamic response = new ExpandoObject();
        response.Price = 123.45M;
        response.IsSuccess = true;
        response.Name = "Test Name";
        response.Tax = 123.45M * 0.12M;

        return response;
    }
}