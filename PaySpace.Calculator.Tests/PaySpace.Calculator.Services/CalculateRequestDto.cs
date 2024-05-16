using NUnit.Framework;
using PaySpace.Calculator.Services.Models.Dto;

namespace PaySpace.Calculator.Tests.Dtos;

[TestFixture]
internal sealed class CalculateRequestDtoTests
{
    [TestCase("12345", 1000)]
    [TestCase(null, 5000)]
    public void CalculateRequestDto_Should_Set_Properties_Correctly(string? postalCode, decimal income)
    {
        // Arrange & Act
        var dto = new CalculateRequestDto(postalCode, income);

        // Assert
        Assert.That(dto.PostalCode, Is.EqualTo(postalCode));
        Assert.That(dto.Income, Is.EqualTo(income));
    }
}
