using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace EssentialTools.Models {
    public class LinqValueCalculator : IValueCalculator {
        private readonly IDiscountCalculator _discountCalculator;
        private static int counter = 0;

        public LinqValueCalculator(IDiscountCalculator discountCalculator) {
            _discountCalculator = discountCalculator;
            Debug.WriteLine(string.Format("Instance {0} created", ++counter));
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