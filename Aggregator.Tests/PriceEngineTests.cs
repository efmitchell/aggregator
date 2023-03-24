using Aggregator.Models;
using Aggregator.QuotationSystem;
using FluentAssertions;
using Moq;
using Xunit;

namespace Aggregator.Tests;

public class PriceEngineTests
{
    private readonly PriceEngine _priceEngine;
    private readonly RiskData _validRiskData;
    private readonly IEnumerable<IQuotationSystem> _quotationSystems;

    public PriceEngineTests()
    {
        _validRiskData = new RiskData
        {
            FirstName = "John",
            LastName = "Doe",
            Value = 20000,
            Make = "Toyota",
            DOB = new DateTime(1985, 1, 1)
        };

        // Create mock quotation systems
        var mockQuotationSystem1 = new Mock<IQuotationSystem>();
        var mockQuotationSystem2 = new Mock<IQuotationSystem>();
        var mockQuotationSystem3 = new Mock<IQuotationSystem>();

        // Set up the mock quotation systems to return sample price responses
        mockQuotationSystem1.Setup(qs => qs.GetPriceAsync(_validRiskData))
            .ReturnsAsync(new PriceResponse { Price = 100 });
        mockQuotationSystem2.Setup(qs => qs.GetPriceAsync(_validRiskData))
            .ReturnsAsync(new PriceResponse { Price = 200 });
        mockQuotationSystem3.Setup(qs => qs.GetPriceAsync(_validRiskData))
            .ReturnsAsync(new PriceResponse { Price = 300 });

        // Create a list of quotation systems and pass it to the PriceEngine constructor
        _quotationSystems = new List<IQuotationSystem>
        {
            mockQuotationSystem1.Object,
            mockQuotationSystem2.Object,
            mockQuotationSystem3.Object
        };

        _priceEngine = new PriceEngine(_quotationSystems);
    }

    private PriceRequest CreatePriceRequest(RiskData riskData)
    {
        return new PriceRequest { RiskData = riskData };
    }

    [Fact]
    public async Task GetPriceAsync_WithValidRiskData_ReturnsPrice()
    {
        // Arrange
        var priceRequest = CreatePriceRequest(_validRiskData);

        // Act
        var result = await _priceEngine.GetPriceAsync(priceRequest);

        // Assert
        result.Error.Should().BeNull();
        result.Price.Should().BeGreaterThan(0);
    }

    [Fact]
    public async Task GetPriceAsync_WithNoFirstName_ReturnsError()
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
        var priceRequest = CreatePriceRequest(riskData);

        // Act
        var result = await _priceEngine.GetPriceAsync(priceRequest);

        // Assert
        result.Error.Should().Be("First name is required");
        result.Price.Should().Be(-1);
    }

    [Fact]
    public async Task GetPriceAsync_WithNoLastName_ReturnsError()
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
        var priceRequest = CreatePriceRequest(riskData);

        // Act
        var result = await _priceEngine.GetPriceAsync(priceRequest);

        // Assert
        result.Error.Should().Be("Surname is required");
        result.Price.Should().Be(-1);
    }

    [Fact]
    public async Task GetPriceAsync_WithNoMake_ReturnsError()
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
        var priceRequest = CreatePriceRequest(riskData);

        // Act
        var result = await _priceEngine.GetPriceAsync(priceRequest);

        // Assert
        result.Error.Should().Be("Make is required");
        result.Price.Should().Be(-1);
    }

    [Fact]
    public async Task GetPriceAsync_WithMissingValue_ReturnsError()
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
        var priceRequest = CreatePriceRequest(riskData);

        // Act
        var result = await _priceEngine.GetPriceAsync(priceRequest);

        // Assert
        result.Error.Should().Be("Value is required");
        result.Price.Should().Be(-1);
    }
}