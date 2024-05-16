namespace PaySpace.Calculator.Services.Abstractions;

public interface IRateCalculator
{
    /// <summary>
    /// Calculate tax according to income
    /// </summary>
    /// <param name="income">Income</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns><see cref="CalculateResult"/></returns>
    Task<CalculateResult> CalculateAsync(decimal income, CancellationToken cancellationToken);
}