# Aggregator

A gadget insurance price aggregator that helps users find the best insurance price for their gadgets by collecting quotes from multiple external quotation systems.

## Overview

The aggregator validates user input, queries multiple external quotation systems for insurance prices, selects the best price from the available quotations, and handles errors and exceptions in the validation and quotation retrieval process.

## Usage

1. Creates a `RiskData` object containing the details of the gadget to be insured, including first name, last name, value, make, and date of birth.
2. Initialize the `PriceEngine` with a list of external quotation systems implementing the `IQuotationSystem` interface.
3. Call the `GetPriceAsync` method of the `PriceEngine` with a `PriceRequest` containing the `RiskData`.
4. The `PriceEngine` returns a `PriceResponse` with the best available insurance price and any errors encountered during the process.

## Running Tests

The project includes unit tests for the `RiskData` and `PriceEngine` components to ensure proper functionality and error handling. To run the tests, use the test runner provided by your IDE or run the `dotnet test` command in the terminal, targeting the test project.