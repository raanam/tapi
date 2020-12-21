using System;
using System.Collections.Generic;
using System.Linq;
using TrolleyApi.ApiProxies;

namespace TrolleyApi.TrolleyTotal
{
    public interface INormalPriceProcessor
    {
        decimal Calculate(PurchasedQuantity purchasedQuantity, List<Product> products);
    }

    public class NormalPriceProcessor : INormalPriceProcessor
    {
        public decimal Calculate(PurchasedQuantity purchasedQuantity, List<Product> products)
        {
            if (purchasedQuantity.QuantityRemainingToBeBilled <= 0)
                return 0m;

            var cost = products
                .Where(p => p.Name == purchasedQuantity.Name)
                .Select(p => p.Price)
                .FirstOrDefault();

            return Convert.ToDecimal(purchasedQuantity.QuantityRemainingToBeBilled) * cost;
        }
    }
}
