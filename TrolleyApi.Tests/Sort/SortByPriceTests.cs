using AutoFixture;
using FluentAssertions;
using System.Linq;
using System.Threading.Tasks;
using TrolleyApi.ApiProxies;
using TrolleyApi.Sort;
using TrolleyApi.Tests.Common;
using Xunit;

namespace TrolleyApi.Tests.Sort
{
    public class SortByPriceTests
    {
        private readonly IProductSortService _sut;
        private readonly IFixture _fixture;

        public SortByPriceTests()
        {
            _fixture = FixtureBuilder.Build();
            _sut = _fixture.Create<SortByPrice>();
        }

        [Fact]
        public async Task Given_ValidProducts_When_SortWithHigh_ReturnsSortedByPrice()
        {
            // Arrange.
            var products = _fixture.CreateMany<Product>(2).ToList();
            products[0].Price = 50.55M;
            products[1].Price = 60.55M;

            // Act.
            var sorted = await _sut.Sort(SortOptions.HIGH, products);

            // Assert.
            sorted[0].Should().Be(products[1]);
            sorted[1].Should().Be(products[0]);
        }

        [Fact]
        public async Task Given_ValidProducts_When_SortWithLow_ReturnsSortedByPrice()
        {
            // Arrange.
            var products = _fixture.CreateMany<Product>(2).ToList();
            products[0].Price = 50.55M;
            products[1].Price = 25.55M;

            // Act.
            var sorted = await _sut.Sort(SortOptions.LOW, products);

            // Assert.
            sorted[0].Should().Be(products[1]);
            sorted[1].Should().Be(products[0]);
        }
    }
}
