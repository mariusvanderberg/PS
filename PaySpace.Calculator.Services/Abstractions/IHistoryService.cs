using PaySpace.Calculator.Services.Helpers;

namespace PaySpace.Calculator.Services.Abstractions;

public interface IHistoryService
{
    /// <summary>
    /// Get all tax calculation history
    /// </summary>
    /// <param name="query"><see cref="QueryRequest"/></param>
    /// <returns><see cref="IQueryable"/> of <seealso cref="CalculatorHistory"/></returns>
    IQueryable<CalculatorHistory> GetHistoryQuery(QueryRequest query);

    /// <summary>
    /// Add tax calculation history
    /// </summary>
    /// <param name="calculatorHistory"><see cref="CalculatorHistory"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>void</returns>
    Task AddAsync(CalculatorHistory calculatorHistory, CancellationToken cancellationToken);
}