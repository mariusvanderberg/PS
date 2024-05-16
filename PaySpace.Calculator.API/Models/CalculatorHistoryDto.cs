namespace PaySpace.Calculator.API.Models;

public sealed record CalculatorHistoryDto(string PostalCode, DateTime Timestamp, decimal Income, decimal Tax, string Calculator);