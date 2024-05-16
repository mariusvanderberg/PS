using PaySpace.Calculator.Services.Exceptions;

namespace PaySpace.Calculator.Services.Calculators;

internal sealed class FlatValueCalculator(ICalculatorSettingsService calculatorSettingsService) : IFlatValueCalculator
{
    /// <inheritdoc/>
    public async Task<CalculateResult> CalculateAsync(decimal income, CancellationToken cancellationToken)
    {
        var settings = await calculatorSettingsService.GetSettingsAsync(CalculatorType.FlatValue);
        if (settings.Count == 0) throw new SettingsException();

        decimal taxAmount;
        var rate = settings.Where(x => x.Calculator == CalculatorType.FlatValue && x.RateType == RateType.Amount && income >= x.From);
        
        if (rate.Any())
        {
            taxAmount = rate.FirstOrDefault()!.Rate;
        }
        else
        {
            rate = settings.Where(x => x.RateType == RateType.Percentage && income >= x.From && income <= x.To + 0.99m);
            if (!rate.Any()) throw new SettingsException();
            taxAmount = income * (rate.FirstOrDefault()!.Rate / 100);
        }

        return new CalculateResult
        {
            Calculator = CalculatorType.FlatValue,
            Tax = taxAmount
        };
    }
}