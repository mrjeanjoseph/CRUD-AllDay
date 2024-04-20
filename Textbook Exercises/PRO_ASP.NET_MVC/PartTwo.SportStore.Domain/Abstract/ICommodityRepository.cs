using System.Collections.Generic;

namespace SportStore.Domain {
    public interface ICommodityRepository {
        IEnumerable<Commodity> Commodities { get; } 
    }
}
