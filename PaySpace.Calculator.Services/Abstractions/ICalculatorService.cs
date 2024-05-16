namespace PaySpace.Calculator.Services.Abstractions;

public interface ICalculatorService
{
    /// <summary>
    /// Calculate tax according to provided details
    /// </summary>
    /// <param name="requestDto"><see cref="CalculateRequestDto"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns><see cref="CalculateResult"/></returns>
    Task<CalculateResult> CalculateTaxAsync(CalculateRequestDto requestDto, CancellationToken cancellationToken);
}