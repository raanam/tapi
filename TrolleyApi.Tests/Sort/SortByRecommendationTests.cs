using System;
using System.Collections.Generic;
using System.Text;
using NSubstitute;
using AutoFixture;
using TrolleyApi.Tests.Common;
using TrolleyApi.ApiProxies;
using TrolleyApi.Sort;
using Xunit;
using System.Threading.Tasks;
using FluentAssertions;

namespace TrolleyApi.Tests.Sort
{
    public class SortByRecommendationTests
    {
        private readonly SortByRecommendation _sut;

        private readonly IFixture _fixture;
        
        private readonly IShopperHistoryRepository _shopperHistoryRepository;
        
        public SortByRecommendationTests()
        {
            _fixture = FixtureBuilder.Build();
            _shopperHistoryRepository = _fixture.Freeze<IShopperHistoryRepository>();
            _sut = _fixture.Create<SortByRecommendation>();
        }

        [Fact]
        public async Task Given_ShopperHistory_When_Sort_Returns_MostBoughtProductsFirst()
        {
            // Arrange.
            var shopperHistory = new List<ShopperHistoryResponse>
            {
                new ShopperHistoryResponse {
                    CustomerId = "1",
                    Products = new List<Product>
                    {
                        new Product { Name = "a", Quantity = 2d },
                        new Product { Name = "b", Quantity = 3d }
                    }
                },
                new ShopperHistoryResponse {
                    CustomerId = "2",
                    Products = new List<Product>
                    {
                        new Product { Name = "a", Quantity = 1d },
                        new Product { Name = "b", Quantity = 1d },
                        new Product { Name = "c", Quantity = 1d }
                    }
                },
            };

            _shopperHistoryRepository.Get(Arg.Any<string>()).Returns(shopperHistory);

            var productsToSort = new List<Product>
            {
                new Product { Name = "a" },
                new Product { Name = "b" },
                new Product { Name = "d" },
            };

            // Act.
            var result = await _sut.Sort(SortOptions.RECOMMENDED, productsToSort);

            // Assert.
            result[0].Should().Be(productsToSort[1]);
            result[1].Should().Be(productsToSort[0]);
            result[2].Should().Be(productsToSort[2]);
        }
    }
}
