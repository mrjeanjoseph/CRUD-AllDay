using System.Collections.Generic;

namespace SportsStore.Domain
{
    public class EFMerchRepository : IMerchRepo
    {
        private readonly SportsStoreDbContext context = new SportsStoreDbContext();
        public IEnumerable<Merchandise> Merch
        {
            get { return context.Merchandise_One; }
        }
    }
}
