using System.Collections.Generic;

namespace TrolleyApi.ApiProxies
{
    public class ShopperHistoryResponse
    {
        public string CustomerId { get; set; }
        public List<Product> Products { get; set; }
    }
}
