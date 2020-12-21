using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using NSubstitute;
using TrolleyApi.Sort;
using AutoFixture;
using TrolleyApi.Tests.Common;
using TrolleyApi.ApiProxies;
using System.Linq;
using FluentAssertions;
using System.Threading.Tasks;

namespace TrolleyApi.Tests.Sort
{
    public class SortServiceTests
    {
        private readonly ISortService _sut;
        private readonly IFixture _fixture;

        private readonly IProductSortService _productSort1;
        private readonly IProductSortService _productSort2;
        private readonly IProductsRepository _productsRepository; 

        public SortServiceTests()
        {
            _fixture = FixtureBuilder.Build();

            _productSort1 = _fixture.Create<IProductSortService>();
            _productSort2 = _fixture.Create<IProductSortService>();

            _fixture.Inject<IReadOnlyList<IProductSortService>>(new List<IProductSortService> 
            {
                _productSort1,
                _productSort2
            });

            _productsRepository = _fixture.Freeze<IProductsRepository>();
            _sut = _fixture.Create<SortService>();
        }

        [Fact]
        public async Task Given_ValidInputs_When_Sort_Returns_SortedProducts()
        {
            // Arrange.
            _productSort1.SupportedSortOptions.Returns(new List<SortOptions> { SortOptions.ASCENDING, SortOptions.DESCENDING });
            _productSort2.SupportedSortOptions.Returns(new List<SortOptions> { SortOptions.HIGH, SortOptions.LOW });

            var products = _fixture.CreateMany<Product>(5).ToList();

            _productsRepository.Get(Arg.Any<string>()).Returns(products);

            _productSort1.Sort(Arg.Any<SortOptions>(), Arg.Is<List<Product>>(products)).Returns(products);

            // Act.
            var result = await _sut.Sort("ASCENDING");

            // Assert.
            result.SequenceEqual(products);
        }
    }
}
