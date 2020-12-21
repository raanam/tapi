using System.Collections.Generic;
using System.Threading.Tasks;
using TrolleyApi.ApiProxies;

namespace TrolleyApi.Sort
{
    public interface IProductSortService
    {
        IReadOnlyList<SortOptions> SupportedSortOptions { get; }

        Task<IReadOnlyList<Product>> Sort(SortOptions option, List<Product> products);
    }
}
