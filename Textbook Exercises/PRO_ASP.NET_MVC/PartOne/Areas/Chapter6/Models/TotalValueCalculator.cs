using System.Collections.Generic;
using System.Linq;

namespace EssentialTools.Models {
    public class TotalValueCalculator {
        public decimal MerchandiseValue(IEnumerable<Merchandise> merchandises) {
            return merchandises.Sum(m => m.Price);
        }
    }
}