namespace PaySpace.Calculator.Services.Abstractions;

public interface ICalculatorTypeService
{
    /// <summary>
    /// Get Calculator type by postal code
    /// </summary>
    /// <param name="code"></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns><see cref="CalculatorType"/></returns>
    Task<CalculatorType?> GetCalculatorTypeByCodeAsync(string code, CancellationToken cancellationToken);
}