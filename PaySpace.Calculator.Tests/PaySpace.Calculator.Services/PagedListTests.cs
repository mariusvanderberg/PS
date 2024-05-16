using MapsterMapper;
using NUnit.Framework;
using PaySpace.Calculator.API.Models;
using PaySpace.Calculator.Data;
using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Services.Helpers;

namespace PaySpace.Calculator.Tests
{
    [TestFixture]
    internal sealed class PagedListTests : TestBase
    {
        private IMapper _mapper;
        private CalculatorContext _context;

        [SetUp]
        public void SetUp()
        {
            _mapper = this.Resolve<IMapper>() ?? 
                throw new ArgumentNullException(Constants.Format.FormatResolveError(nameof(IMapper)));
            _context = this.Resolve<CalculatorContext>() ?? 
                throw new ArgumentNullException(Constants.Format.FormatResolveError(nameof(CalculatorContext)));
        }

        [Test]
        public async Task CreateAsync_Should_Return_Paginated_List()
        {
            // Arrange
            int page = 2, pageSize = 9, total = 15;

            for (int i = 0; i < total; i++)
            {
                _context.Set<CalculatorHistory>().Add(new()
                {
                    Id = i + 1,
                    Income = 1000 * (i + 1),
                    PostalCode = "7441",
                    Tax = 100 * (i + 1),
                    Calculator = CalculatorType.Progressive
                }); // Unlikely, but we just want the data
            }

            await _context.SaveChangesAsync();

            var query = _context.Set<CalculatorHistory>().AsQueryable();
            var request = new QueryRequest { Page = page, PageSize = pageSize };

            // Act
            var pagedList = await PagedList<CalculatorHistory>.CreateAsync<CalculatorHistoryDto>(_mapper, query, request, CancellationToken.None);

            // Assert
            Assert.That(pagedList.Data, Has.Count.EqualTo(6));
            Assert.That(pagedList.Data.First().Income, Is.EqualTo(10000M)); // First item on the second page
            Assert.That(pagedList.Total, Is.EqualTo(total));
            Assert.That(pagedList.Page, Is.EqualTo(page));
            Assert.That(pagedList.PageSize, Is.EqualTo(pageSize));
        }

        [Test]
        public void Create_Should_Return_Paginated_List()
        {
            // Arrange
            var query = Enumerable.Range(1, 100).ToList();

            // Act
            var pagedList = PagedList<int>.Create<int>(_mapper, query);

            // Assert
            Assert.That(pagedList.Data, Has.Count.EqualTo(100));
            Assert.That(pagedList.Data.First(), Is.EqualTo(1));
            Assert.That(pagedList.Total, Is.EqualTo(100));
            Assert.That(pagedList.Page, Is.EqualTo(1));
            Assert.That(pagedList.PageSize, Is.EqualTo(100));
        }

        [TearDown]
        public void TearDown()
        {
            _context?.Dispose();
        }
    }
}