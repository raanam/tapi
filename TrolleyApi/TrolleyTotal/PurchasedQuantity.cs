using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TrolleyApi.TrolleyTotal
{
    public class PurchasedQuantity
    {
        public string Name { get; set; }
        public double Quantity { get; set; }

        [JsonIgnore]
        public double QuantityCalculatedForBill { get; private set; }

        public double QuantityRemainingToBeBilled => Quantity - QuantityCalculatedForBill;

        public void MarkQuantityAsBilled(double quantity)
        {
            if (QuantityCalculatedForBill + quantity > Quantity)
                throw new ValidationException("Cannot mark more than purchased");

            QuantityCalculatedForBill += quantity;
        }
    }
}
