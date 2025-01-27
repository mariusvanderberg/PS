﻿using NUnit.Framework;

namespace PaySpace.Calculator.Tests
{
    [TestFixture]
    internal sealed class ProgressiveCalculatorTests : TestBase
    {
        private IProgressiveCalculator? _progressiveCalculator;

        [TestCase(-1, 0)]
        [TestCase(50, 5)]
        [TestCase(8350.1, 835.01)]
        [TestCase(8351, 835)]
        [TestCase(33951, 4674.85)]
        [TestCase(82251, 16749.60)]
        [TestCase(171550, 41753.32)]
        [TestCase(999999, 327681.79)]
        public async Task Calculate_Should_Return_Expected_Tax(decimal income, decimal expectedTax)
        {
            // Arrange
            _progressiveCalculator ??= this.Resolve<IProgressiveCalculator>();

            // Act
            var result = await _progressiveCalculator.CalculateAsync(income, CancellationToken.None);

            // Assert
            Assert.That(result.Tax, Is.EqualTo(expectedTax));
        }
    }
}