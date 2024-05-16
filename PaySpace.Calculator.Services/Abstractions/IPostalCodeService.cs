namespace PaySpace.Calculator.Services.Abstractions;

public interface IPostalCodeService
{
    /// <summary>
    /// Get all postal codes
    /// </summary>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>A list of postal codes</returns>
    Task<List<PostalCode>> GetPostalCodes(CancellationToken cancellationToken);
}