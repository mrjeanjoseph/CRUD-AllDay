using System.Collections.Generic;

namespace SportsStore.Domain
{
    public class EFMerchRepository : IMerchRepo
    {
        private readonly EFDbContext context = new EFDbContext();
        public IEnumerable<Merchandise> Merch
        {
            get { return context.Merchandise_One; }
        }
    }
}
