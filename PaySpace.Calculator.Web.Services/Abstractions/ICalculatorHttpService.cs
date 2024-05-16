using PaySpace.Calculator.Web.Services.Helpers;

namespace PaySpace.Calculator.Web.Services.Abstractions;

public interface ICalculatorHttpService
{
    /// <summary>
    /// Gets all postal codes
    /// </summary>
    /// <returns><see cref="PagedList"/> of <see cref="PostalCode"/></returns>
    Task<PagedList<PostalCode>> GetPostalCodesAsync();

    /// <summary>
    /// Gets full tax calculation history
    /// </summary>
    /// <param name="request"><see cref="QueryRequest"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns><see cref="PagedList"/> of <see cref="CalculatorHistory"/></returns>
    Task<PagedList<CalculatorHistory>> GetHistoryAsync(QueryRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Calculate tax according to postal code and income
    /// </summary>
    /// <param name="calculationRequest"><see cref="CalculateRequest"/></param>
    /// <returns><see cref="CalculateResult"/></returns>
    Task<CalculateResult?> CalculateTaxAsync(CalculateRequest calculationRequest);
}