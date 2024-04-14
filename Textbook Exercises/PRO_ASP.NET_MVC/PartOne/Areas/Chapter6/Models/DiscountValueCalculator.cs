namespace EssentialTools.Models {
    public class DiscountValueCalculator : IDiscountCalculator {
        public decimal ApplyDiscount(decimal totalParam) {
            return (totalParam - (10m / 10m * totalParam));
        }
    }
}