using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrolleyApi.ApiProxies;

namespace TrolleyApi.Sort
{
    public interface ISortService
    {
        Task<IReadOnlyList<Product>> Sort(string sortOption);
    }

    public class SortService : ISortService
    {
        private readonly IConfiguration _configuration;
        private readonly IProductsRepository _productsRepository;
        private readonly IReadOnlyList<IProductSortService> _sortServices;

        public SortService(
            IConfiguration configuration, 
            IProductsRepository productsRepository,
            IReadOnlyList<IProductSortService> sortServices)
        {
            _configuration = configuration;
            _productsRepository = productsRepository;
            _sortServices = sortServices;
        }

        public async Task<IReadOnlyList<Product>> Sort(string sortOption)
        {
            var allProducts = await _productsRepository.Get(_configuration["UserToken"]);
            
            var parsedSortOption = (SortOptions) Enum.Parse(typeof(SortOptions), sortOption);

            var sortService = _sortServices.FirstOrDefault(svc => svc.SupportedSortOptions.Contains(parsedSortOption));

            return await sortService.Sort(parsedSortOption, allProducts);
        }
    }
}
