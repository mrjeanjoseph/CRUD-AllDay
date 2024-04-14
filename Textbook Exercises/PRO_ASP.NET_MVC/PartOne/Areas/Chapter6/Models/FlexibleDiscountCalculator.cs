namespace EssentialTools.Models {
    public class FlexibleDiscountCalculator : IDiscountCalculator {
        public decimal ApplyDiscount(decimal totalParam) {
            decimal discount = totalParam > 100 ? 70 : 25;
            return (totalParam - (discount / 100m * totalParam));
        }
    }
}