using PaySpace.Calculator.Services.Common;
using PaySpace.Calculator.Services.Exceptions;

namespace PaySpace.Calculator.Services.Calculators;

internal sealed class FlatRateCalculator(ICalculatorSettingsService calculatorSettingsService) : IFlatRateCalculator
{
    /// <inheritdoc/>
    public async Task<CalculateResult> CalculateAsync(decimal income, CancellationToken cancellationToken)
    {
        var settings = await calculatorSettingsService.GetSettingsAsync(CalculatorType.FlatRate);
        if (settings.Count == 0) throw new SettingsException();

        CalculatorSetting rate = settings.FirstOrDefault(x => income >= x.From && (x.To is null || income <= x.To)) ?? throw new ArgumentNullException(Constants.NO_SETTINGS_FOUND);
        decimal taxAmount = income * (rate.Rate / 100);

        return new CalculateResult
        {
            Calculator = CalculatorType.FlatRate,
            Tax = taxAmount
        };
    }
}