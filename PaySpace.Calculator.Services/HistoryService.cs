using PaySpace.Calculator.Data;
using PaySpace.Calculator.Services.Helpers;
using System.Linq.Expressions;

namespace PaySpace.Calculator.Services;

internal sealed class HistoryService(CalculatorContext context) : IHistoryService
{
    /// <inheritdoc/>
    public async Task AddAsync(CalculatorHistory calculatorHistory, CancellationToken cancellationToken)
    {
        if (calculatorHistory is null)
        {
            throw new ArgumentNullException(nameof(calculatorHistory));
        }

        calculatorHistory.Timestamp = DateTime.Now;

        await context.AddAsync(calculatorHistory, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public IQueryable<CalculatorHistory> GetHistoryQuery(QueryRequest query)
    {
        var historyQuery = context.Set<CalculatorHistory>().AsQueryable();

        var keySelector = GetKeySelector(query.SortColumn);

        if (query.SortOrder?.ToLower() == "desc")
            historyQuery = historyQuery.OrderByDescending(keySelector);
        else
            historyQuery = historyQuery.OrderBy(keySelector);

        return historyQuery;
    }

    /// <summary>
    /// Example of a key selector; Can be moved to a general helper that uses reflection to get the key selector
    /// </summary>
    /// <param name="sortColumn">Column for sorting</param>
    /// <returns>Expression<Func<CalculatorHistory, object>></returns>
    private Expression<Func<CalculatorHistory, object>> GetKeySelector(string? sortColumn)
    {
        return sortColumn switch
        {
            nameof(CalculatorHistory.Id) => s => s.Id,
            nameof(CalculatorHistory.Calculator) => s => s.Calculator,
            nameof(CalculatorHistory.PostalCode) => s => s.PostalCode,
            nameof(CalculatorHistory.Tax) => s => s.Tax,
            nameof(CalculatorHistory.Income) => s => s.Income,
            nameof(CalculatorHistory.Timestamp) => s => s.Timestamp,
            _ => s => s.Timestamp
        };
    }
}