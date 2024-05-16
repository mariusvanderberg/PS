using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using PaySpace.Calculator.Data;

namespace PaySpace.Calculator.Services;

internal sealed class CalculatorSettingsService(CalculatorContext context, IMemoryCache memoryCache) : ICalculatorSettingsService
{
    /// <inheritdoc/>
    public Task<List<CalculatorSetting>> GetSettingsAsync(CalculatorType calculatorType)
    {
        return memoryCache.GetOrCreateAsync($"CalculatorSetting:{calculatorType}", async entry =>
        {
            return await context.Set<CalculatorSetting>().AsNoTracking().Where(_ => _.Calculator == calculatorType).ToListAsync();
        })!;
    }
}