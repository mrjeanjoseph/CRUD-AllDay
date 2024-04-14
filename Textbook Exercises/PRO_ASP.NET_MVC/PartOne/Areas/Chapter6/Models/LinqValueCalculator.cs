using System.Collections.Generic;
using System.Linq;

namespace EssentialTools.Models {
    public class LinqValueCalculator : IValueCalculator {
        private readonly IDiscountCalculator _discountCalculator;

        public LinqValueCalculator(IDiscountCalculator discountCalculator) {
            _discountCalculator = discountCalculator;
        }
        public decimal MerchandiseValue(IEnumerable<Merchandise> merchandises) {
            return _discountCalculator.ApplyDiscount(
                merchandises.Sum(m => m.Price));
        }
    }

    //Archived
    public class LinqValueCalculator_Arch {
        public decimal MerchandiseValue(IEnumerable<Merchandise> merchandises) {
            return merchandises.Sum(m => m.Price);
        }
    }
}