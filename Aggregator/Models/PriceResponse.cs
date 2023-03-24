namespace Aggregator;

public class PriceResponse
{
    public decimal Tax { get; set; }
    public string Insurer { get; set;}
    public string Error { get; set; } // Confirm if Error string is currently in use and safe to deprecate, replace with more detailed error object
    public decimal Price { get; set; }

}