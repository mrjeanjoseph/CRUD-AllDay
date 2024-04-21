using System.Collections.Generic;

namespace SportsStore.Domain {
    public interface IMerchRepo {
        IEnumerable<Merchandise> Merchandises { get; }
    }
}
