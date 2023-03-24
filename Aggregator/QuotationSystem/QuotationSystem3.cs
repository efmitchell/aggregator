using System.Dynamic;

namespace Aggregator.QuotationSystem;

public class QuotationSystem3 : IQuotationSystem
{
    public QuotationSystem3(string url, string port)
    {

    }

    public Task<dynamic> GetPriceAsync(RiskData riskData)
    {
        //Omitted - Call to an external service
        //var response = _someExternalService.PostHttpRequest(requestData);

        dynamic response = new ExpandoObject();
        response.Price = 92.67M;
        response.IsSuccess = true;
        response.Name = "zxcvbnm";
        response.Tax = 92.67M * 0.12M;

        return response;
    }
}