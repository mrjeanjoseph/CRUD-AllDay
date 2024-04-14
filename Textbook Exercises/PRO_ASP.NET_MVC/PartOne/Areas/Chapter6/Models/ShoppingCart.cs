using System.Collections.Generic;

namespace EssentialTools.Models {
    public class ShoppingCart {
        private readonly TotalValueCalculator _totalValueCalculator;

        public ShoppingCart(TotalValueCalculator calcParam) {
            _totalValueCalculator = calcParam;
        }

        public IEnumerable<Merchandise> merchandises { get; set; }

        public decimal CalculateMerchandiseTotal() {
            return _totalValueCalculator.MerchandiseValue(merchandises);
        }
    }
}