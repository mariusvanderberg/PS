using PaySpace.Calculator.Services.Exceptions;

namespace PaySpace.Calculator.Services.Calculators;

internal sealed class ProgressiveCalculator(ICalculatorSettingsService calculatorSettingsService) : IProgressiveCalculator
{
    /// <inheritdoc/>
    public async Task<CalculateResult> CalculateAsync(decimal income, CancellationToken cancellationToken)
    {
        var settings = await calculatorSettingsService.GetSettingsAsync(CalculatorType.Progressive);
        if (settings.Count == 0) throw new SettingsException();
        decimal taxAmount = 0.0m;
        foreach (var taxlevel in settings.Where(x => income >= x.From).OrderBy(o => o.From))
        {
            if (income <= 0) break;
            if (income > taxlevel.To)
            {
                // Calculate full bracket
                var taxable = (taxlevel.To) - taxlevel.From;
                if (income - taxlevel.To < 1) taxable += income - taxlevel.To;
                taxAmount += taxable * (taxlevel.Rate / 100) ?? 0m;
            }
            else
            {
                // Calculate partial / final
                taxAmount += (income - taxlevel.From) * (taxlevel.Rate / 100);
            }
        }

        return new CalculateResult
        {
            Calculator = CalculatorType.Progressive,
            Tax = taxAmount
        };
    }
}