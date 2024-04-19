namespace EssentialTools.Models {
    public class DiscountValueCalculator : IDiscountCalculator {

        public decimal DiscountSize;
        public DiscountValueCalculator(decimal discountParam) {
            DiscountSize = discountParam;
        }

        public decimal DiscountSize_Arch { get; set; }

        public decimal ApplyDiscount(decimal totalParam) {
            return (totalParam - (DiscountSize / 100m * totalParam));
        }
    }
}