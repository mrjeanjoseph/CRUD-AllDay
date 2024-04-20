using System.Collections.Generic;

namespace SportStore.Domain.Abstract {
    public interface ICommodityRepository {
        IEnumerable<Commodity> Commodities { get; } 
    }
}
