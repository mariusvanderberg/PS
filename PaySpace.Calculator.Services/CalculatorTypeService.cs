namespace PaySpace.Calculator.Services;

internal sealed class CalculatorTypeService(IPostalCodeService postalCodeService) : ICalculatorTypeService
{
    /// <inheritdoc/>
    public async Task<CalculatorType?> GetCalculatorTypeByCodeAsync(string code, CancellationToken cancellationToken)
    {
        var postalCodes = await postalCodeService.GetPostalCodes(cancellationToken);
        var postalCode = postalCodes.FirstOrDefault(pc => pc.Code == code);

        return postalCode?.Calculator;
    }
}