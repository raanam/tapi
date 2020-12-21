using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrolleyApi.ApiProxies;

namespace TrolleyApi.Sort
{
    public class SortByName : IProductSortService
    {
        public IReadOnlyList<SortOptions> SupportedSortOptions =>
            new List<SortOptions> { SortOptions.ASCENDING, SortOptions.DESCENDING };

        public Task<IReadOnlyList<Product>> Sort(SortOptions option, List<Product> products)
        {
            var sorted = option == SortOptions.DESCENDING ?
                 products.OrderByDescending(p => p.Name) :
                 products.OrderBy(p => p.Name);

            return Task.FromResult<IReadOnlyList<Product>>(sorted.ToList());
        }
    }
}
