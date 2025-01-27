﻿using NUnit.Framework;

namespace PaySpace.Calculator.Tests
{
    [TestFixture]
    internal sealed class FlatRateCalculatorTests : TestBase
    {
        private IFlatRateCalculator? _flatRateCalculator;

        [TestCase(999999, 174999.825)]
        [TestCase(1000, 175)]
        [TestCase(5, 0.875)]
        public async Task Calculate_Should_Return_Expected_Tax(decimal income, decimal expectedTax)
        {
            // Arrange
            _flatRateCalculator ??= this.Resolve<IFlatRateCalculator>();

            // Act
            var result = await _flatRateCalculator.CalculateAsync(income, CancellationToken.None);

            // Assert
            Assert.That(result.Tax, Is.EqualTo(expectedTax));
        }
    }
}
