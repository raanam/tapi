namespace TrolleyApi.TrolleyTotal
{
    public interface ITrolleyTotalCalculatorService
    {
        decimal Calculate(CalculateTrolleyTotalRequest request);
    }

    public class TrolleyTotalCalculatorService : ITrolleyTotalCalculatorService
    {
        private readonly ISpecialsProcessor _specialsProcessor;
        private readonly INormalPriceProcessor _normalPriceProcessor;

        public TrolleyTotalCalculatorService(
            ISpecialsProcessor specialsProcessor,
            INormalPriceProcessor normalPriceProcessor)
        {
            _specialsProcessor = specialsProcessor;
            _normalPriceProcessor = normalPriceProcessor;
        }

        public decimal Calculate(CalculateTrolleyTotalRequest request)
        {
            var trolleryTotal = 0m;

            if (request.Specials != null)
            {
                foreach (var eachSpecial in request.Specials)
                {
                    trolleryTotal += _specialsProcessor.Calculate(eachSpecial, request.Quantities);
                }
            }

            foreach (var eachPurchasedQuantity in request.Quantities)
            {
                trolleryTotal += _normalPriceProcessor.Calculate(
                    eachPurchasedQuantity,
                    request.Products);
            }

            return trolleryTotal;
        }
    }
}
