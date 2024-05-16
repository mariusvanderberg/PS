namespace PaySpace.Calculator.Web.Services.Models;

public sealed record CalculatorHistory(string PostalCode, DateTime Timestamp, decimal Income, decimal Tax, string Calculator);