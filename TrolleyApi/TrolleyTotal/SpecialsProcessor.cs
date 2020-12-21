using System.Collections.Generic;
using System.Linq;

namespace TrolleyApi.TrolleyTotal
{
    public interface ISpecialsProcessor
    {
        decimal Calculate(Special special, List<PurchasedQuantity> purchasedQuantities);
    }

    public class SpecialsProcessor : ISpecialsProcessor
    {
        public decimal Calculate(Special special, List<PurchasedQuantity> purchasedQuantities)
        {
            if (!AreMatchingProductsRemaining(special, purchasedQuantities))
                return 0;

            ApplySpecial(special, purchasedQuantities);

            return special.Total;
        }

        private static bool AreMatchingProductsRemaining(
            Special special,
            List<PurchasedQuantity> purchasedQuantities)
        {
            foreach (var eachProductInSpecialGroup in special.Quantities)
            {
                var matchingProductInTrollery = purchasedQuantities
                    .FirstOrDefault(pq => pq.Name == eachProductInSpecialGroup.Name &&
                                  eachProductInSpecialGroup.Quantity > 0 &&
                                  pq.QuantityRemainingToBeBilled >= eachProductInSpecialGroup.Quantity);

                if (matchingProductInTrollery == null)
                    return false;
            }

            return true;
        }

        private void ApplySpecial(
            Special special,
            List<PurchasedQuantity> purchasedQuantities)
        {
            foreach (var eachProductInSpecialGroup in special.Quantities)
            {
                var matchingProductInTrollery = purchasedQuantities
                    .FirstOrDefault(pq => pq.Name == eachProductInSpecialGroup.Name);

                matchingProductInTrollery.MarkQuantityAsBilled(eachProductInSpecialGroup.Quantity);
            }

        }
    }
}
