using System.Collections.Generic;

namespace SportsStore.Domain
{
    public class EFMerchRepo : IMerchandiseRepository
    {
        private readonly SportsStoreDbContext context = new SportsStoreDbContext();
        public IEnumerable<Merchandise> Merchandises
        {
            get { return context.Merchandise; }
        }
    }
}
