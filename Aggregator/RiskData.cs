using Aggregator.Exceptions;

namespace Aggregator;

public class RiskData
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public decimal? Value { get; set; }
    public string Make { get; set; }
    public DateTime DOB { get; set; }
    public string Error { get; set; }

    public (bool, string) Validate()
    {
        if (string.IsNullOrEmpty(FirstName))
        {
            Error = "First name is required";
            throw new ValidationException(Error);
        }

        if (string.IsNullOrEmpty(LastName))
        {
            Error = "Surname is required";
            throw new ValidationException(Error);
        }

        if (Value == null || Value == 0)
        {
            Error = "Value is required";
            throw new ValidationException(Error);
        }

        if (string.IsNullOrEmpty(Make))
        {
            Error = "Make is required";
            throw new ValidationException(Error);
        }

        return (true, null);
    }
}