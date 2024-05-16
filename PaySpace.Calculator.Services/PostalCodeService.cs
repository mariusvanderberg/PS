using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using PaySpace.Calculator.Data;

namespace PaySpace.Calculator.Services;

internal sealed class PostalCodeService(CalculatorContext context, IMemoryCache memoryCache) : IPostalCodeService
{
    /// <inheritdoc/>
    public Task<List<PostalCode>> GetPostalCodes(CancellationToken cancellationToken)
    {
        return memoryCache.GetOrCreateAsync("PostalCodes", _ => context.Set<PostalCode>().AsNoTracking().ToListAsync(cancellationToken))!;
    }
}