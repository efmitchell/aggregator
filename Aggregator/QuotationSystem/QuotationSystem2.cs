using System.Dynamic;

namespace Aggregator.QuotationSystem;

public class QuotationSystem2 : IQuotationSystem
{
    public QuotationSystem2(string url, string port)
    {

    }

    public Task<dynamic> GetPriceAsync(RiskData riskData)
    {
        //Omitted - Call to an external service
        //var response = _someExternalService.PostHttpRequest(requestData);

        dynamic response = new ExpandoObject();
        response.Price = 234.56M;
        response.HasPrice = true;
        response.Name = "qewtrywrh";
        response.Tax = 234.56M * 0.12M;

        return response;
    }
}