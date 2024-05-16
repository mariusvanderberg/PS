namespace PaySpace.Calculator.Services.Abstractions;

public interface ICalculatorSettingsService
{
    /// <summary>
    /// Get calculator settings
    /// </summary>
    /// <param name="calculatorType"><see cref="CalculatorType"/></param>
    /// <returns>A List of <see cref="CalculatorSetting"/></returns>
    Task<List<CalculatorSetting>> GetSettingsAsync(CalculatorType calculatorType);
}