using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TrolleyApi.ApiProxies
{
    public interface IProductsRepository
    {
        [Get("/products?token={token}")]
        public Task<List<Product>> Get(string token);
    }
}
