using Aggregator.Exceptions;
using FluentAssertions;
using Xunit;

namespace Aggregator.Tests;

public class RiskDataTests
{
    private readonly RiskData _validRiskData;

    public RiskDataTests()
    {
        _validRiskData = new RiskData
        {
            FirstName = "John",
            LastName = "Doe",
            Value = 20000,
            Make = "Toyota",
            DOB = new DateTime(1985, 1, 1)
        };
    }

    [Fact]
    public void Validate_WithValidData_DoesNotThrow()
    {
        // Arrange & Act
        Action act = () => _validRiskData.Validate();

        // Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void Validate_WithMissingFirstName_ThrowsValidationException()
    {
        // Arrange
        var riskData = new RiskData
        {
            FirstName = null,
            LastName = _validRiskData.LastName,
            Value = _validRiskData.Value,
            Make = _validRiskData.Make,
            DOB = _validRiskData.DOB
        };

        // Act
        Action act = () => riskData.Validate();

        // Assert
        act.Should().Throw<ValidationException>().WithMessage("First name is required");
    }

    [Fact]
    public void Validate_WithMissingLastName_ThrowsValidationException()
    {
        // Arrange
        var riskData = new RiskData
        {
            FirstName = _validRiskData.FirstName,
            LastName = null,
            Value = _validRiskData.Value,
            Make = _validRiskData.Make,
            DOB = _validRiskData.DOB
        };

        // Act
        Action act = () => riskData.Validate();

        // Assert
        act.Should().Throw<ValidationException>().WithMessage("Surname is required");
    }

    [Fact]
    public void Validate_WithMissingValue_ThrowsValidationException()
    {
        // Arrange
        var riskData = new RiskData
        {
            FirstName = _validRiskData.FirstName,
            LastName = _validRiskData.LastName,
            Value = null,
            Make = _validRiskData.Make,
            DOB = _validRiskData.DOB
        };

        // Act
        Action act = () => riskData.Validate();

        // Assert
        act.Should().Throw<ValidationException>().WithMessage("Value is required");
    }

    [Fact]
    public void Validate_WithValueEqualsZero_ThrowsValidationException()
    {
        // Arrange
        var riskData = new RiskData
        {
            FirstName = _validRiskData.FirstName,
            LastName = _validRiskData.LastName,
            Value = 0,
            Make = _validRiskData.Make,
            DOB = _validRiskData.DOB
        };

        // Act
        Action act = () => riskData.Validate();

        // Assert
        act.Should().Throw<ValidationException>().WithMessage("Value is required");
    }

    [Fact]
    public void Validate_WithMissingMake_ThrowsValidationException()
    {
        // Arrange
        var riskData = new RiskData
        {
            FirstName = _validRiskData.FirstName,
            LastName = _validRiskData.LastName,
            Value = _validRiskData.Value,
            Make = null,
            DOB = _validRiskData.DOB
        };

        // Act
        Action act = () => riskData.Validate();

        // Assert
        act.Should().Throw<ValidationException>().WithMessage("Make is required");
    }
}