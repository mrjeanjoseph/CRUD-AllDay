namespace EssentialTools.Models {
    public interface IDiscountCalculator {
        decimal ApplyDiscount(decimal totalParam);
    }
}