using PaySpace.Calculator.Services.Exceptions;

namespace PaySpace.Calculator.Services.Calculators;

internal sealed class CalculatorService(
    ICalculatorTypeService calculatorTypeService,
    IProgressiveCalculator progressiveCalculatorService,
    IFlatRateCalculator flatRateCalculator,
    IFlatValueCalculator flatValueCalculator) : ICalculatorService
{
    /// <inheritdoc/>
    public async Task<CalculateResult> CalculateTaxAsync(
        CalculateRequestDto requestDto,
        CancellationToken cancellationToken)
    {
        var calculatorType = !string.IsNullOrEmpty(requestDto?.PostalCode)
                ? await calculatorTypeService.GetCalculatorTypeByCodeAsync(requestDto.PostalCode, cancellationToken) ?? throw new CalculatorException()
                : throw new CalculatorException();

        CalculateResult? result = calculatorType switch
        {
            CalculatorType.Progressive => await progressiveCalculatorService.CalculateAsync(requestDto.Income, cancellationToken),
            CalculatorType.FlatValue => await flatValueCalculator.CalculateAsync(requestDto.Income, cancellationToken),
            CalculatorType.FlatRate => await flatRateCalculator.CalculateAsync(requestDto.Income, cancellationToken),
            _ => throw new CalculatorException(),
        };
        return result;
    }
}