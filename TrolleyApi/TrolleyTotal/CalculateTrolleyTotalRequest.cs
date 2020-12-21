using System.Collections.Generic;
using TrolleyApi.ApiProxies;

namespace TrolleyApi.TrolleyTotal
{
    public class CalculateTrolleyTotalRequest
    {
        public List<Product> Products { get; set; }

        public List<Special> Specials { get; set; }

        public List<PurchasedQuantity> Quantities { get; set; }
    }
}
