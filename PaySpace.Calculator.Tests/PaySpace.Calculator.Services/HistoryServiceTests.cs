using NUnit.Framework;
using PaySpace.Calculator.Data;
using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Services;
using PaySpace.Calculator.Services.Helpers;

namespace PaySpace.Calculator.Tests
{
    [TestFixture]
    internal sealed class HistoryServiceTests : TestBase
    {
        private HistoryService _service;
        private CalculatorContext _context;

        [SetUp]
        public void SetUp()
        {
            _context = this.Resolve<CalculatorContext>() ?? 
                throw new ArgumentNullException(Constants.Format.FormatResolveError(nameof(CalculatorContext)));
            _service = new HistoryService(_context);
        }

        [Test]
        public async Task GetHistoryQuery_Should_Return_Sorted_History()
        {
            // Arrange
            var queryRequest = new QueryRequest { SortColumn = "Income", SortOrder = "desc" };
            var histories = new List<CalculatorHistory>
            {
                new() { Id = 1, PostalCode = "7441", Income = 1000, Tax = 175, Calculator = CalculatorType.Progressive, Timestamp = DateTime.Now },
                new() { Id = 2, PostalCode = "A100", Income = 2000, Tax = 350, Calculator = CalculatorType.FlatValue, Timestamp = DateTime.Now },
                new() { Id = 3, PostalCode = "7000", Income = 3000, Tax = 525, Calculator = CalculatorType.FlatRate, Timestamp = DateTime.Now },
                new() { Id = 4, PostalCode = "1000", Income = 4000, Tax = 700, Calculator = CalculatorType.Progressive, Timestamp = DateTime.Now }
            };

            await _context.Set<CalculatorHistory>().AddRangeAsync(histories); // Unlikely, but we just want the data
            await _context.SaveChangesAsync();

            // Act
            var result = _service.GetHistoryQuery(queryRequest);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.First().Income, Is.EqualTo(4000)); // Highest income should be first
            Assert.That(result.Last().Income, Is.EqualTo(1000)); // Lowest income should be last
        }

        [TearDown]
        public void TearDown()
        {
            _context?.Dispose();
        }
    }
}