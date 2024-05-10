using System.Collections.Generic;

namespace SportsStore.Domain
{
    public interface IMerchandiseRepository {
        IEnumerable<Merchandise> Merchandises { get; }
        void SaveMerchandise(Merchandise merchandise);
    }
}
