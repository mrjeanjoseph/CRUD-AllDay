using System.Collections.Generic;

namespace EssentialTools.Models {
    public interface IValueCalculator {
        decimal MerchandiseValue(IEnumerable<Merchandise> merchandises);
    }
}