using System.Collections.Generic;

namespace EssentialTools.Models {

    public class ShoppingCart {
        private readonly IValueCalculator _calculate;

        public ShoppingCart(IValueCalculator calcParam) {
            _calculate = calcParam;
        }

        public IEnumerable<Merchandise> merchandises { get; set; }

        public decimal CalculateMerchandiseTotal() {
            return _calculate.MerchandiseValue(merchandises);
        }
    }

    //Archived
    public class ShoppingCart_Arch {
        private readonly LinqValueCalculator_Arch _totalValueCalculator;

        public ShoppingCart_Arch(LinqValueCalculator_Arch calcParam) {
            _totalValueCalculator = calcParam;
        }

        public IEnumerable<Merchandise> merchandises { get; set; }

        public decimal CalculateMerchandiseTotal() {
            return _totalValueCalculator.MerchandiseValue(merchandises);
        }
    }
}