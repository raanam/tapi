using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TrolleyApi.ApiProxies
{
    public interface IShopperHistoryRepository
    {
        [Get("/shopperHistory?token={token}")]
        public Task<List<ShopperHistoryResponse>> Get(string token);
    }
}
