using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrolleyApi.ApiProxies;

namespace TrolleyApi.Sort
{
    public class SortByPrice : IProductSortService
    {
        public IReadOnlyList<SortOptions> SupportedSortOptions => 
            new List<SortOptions> { SortOptions.HIGH, SortOptions.LOW };

        public Task<IReadOnlyList<Product>> Sort(SortOptions option, List<Product> products)
        {
            var sorted = option == SortOptions.HIGH ?
                 products.OrderByDescending(p => p.Price) :
                 products.OrderBy(p => p.Price);

            return Task.FromResult<IReadOnlyList<Product>>(sorted.ToList());
        }
    }
}
