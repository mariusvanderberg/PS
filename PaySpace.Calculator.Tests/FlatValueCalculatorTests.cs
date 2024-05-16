using NUnit.Framework;

namespace PaySpace.Calculator.Tests
{
    [TestFixture]
    internal sealed class FlatValueCalculatorTests : TestBase
    {
        private IFlatValueCalculator? _flatValueCalculator;

        [TestCase(199999, 9999.95)]
        [TestCase(100, 5)]
        [TestCase(200000, 10000)]
        [TestCase(6000000, 10000)]
        public async Task Calculate_Should_Return_Expected_Tax(decimal income, decimal expectedTax)
        {
            // Arrange
            _flatValueCalculator ??= this.Resolve<IFlatValueCalculator>();

            // Act
            var result = await _flatValueCalculator.CalculateAsync(income, CancellationToken.None);

            // Assert
            Assert.That(result.Tax, Is.EqualTo(expectedTax));
        }
    }
}