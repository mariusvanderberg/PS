using NUnit.Framework;
using PaySpace.Calculator.Data;
using PaySpace.Calculator.Data.Models;

namespace PaySpace.Calculator.Tests
{
    [TestFixture]
    internal sealed class CalculatorContextTests : TestBase
    {
        private CalculatorContext _context;

        [SetUp]
        public void SetUp()
        {
            _context = this.Resolve<CalculatorContext>() ?? 
                throw new ArgumentNullException(Constants.Format.FormatResolveError(nameof(CalculatorContext)));
        }

        [Test]
        public void PostalCodes_Should_Be_Seeded_Correctly()
        {
            // Act
            var postalCodes = _context.Set<PostalCode>().ToList();

            // Assert
            Assert.That(postalCodes, Has.Count.EqualTo(4));
            Assert.That(postalCodes, Has.Exactly(1).Matches<PostalCode>(p => p.Code == "7441" && p.Calculator == CalculatorType.Progressive));
            Assert.That(postalCodes, Has.Exactly(1).Matches<PostalCode>(p => p.Code == "A100" && p.Calculator == CalculatorType.FlatValue));
            Assert.That(postalCodes, Has.Exactly(1).Matches<PostalCode>(p => p.Code == "7000" && p.Calculator == CalculatorType.FlatRate));
            Assert.That(postalCodes, Has.Exactly(1).Matches<PostalCode>(p => p.Code == "1000" && p.Calculator == CalculatorType.Progressive));
        }

        [Test]
        public void CalculatorSettings_Should_Be_Seeded_Correctly()
        {
            // Act
            var calculatorSettings = _context.Set<CalculatorSetting>().ToList();

            // Assert
            Assert.That(calculatorSettings, Has.Count.EqualTo(9));
            Assert.That(calculatorSettings, Has.Exactly(1).Matches<CalculatorSetting>(s => s.Rate == 10 && s.Calculator == CalculatorType.Progressive));
            Assert.That(calculatorSettings, Has.Exactly(1).Matches<CalculatorSetting>(s => s.Rate == 15 && s.Calculator == CalculatorType.Progressive));
        }

        [TearDown]
        public void TearDown()
        {
            _context?.Dispose();
        }
    }
}