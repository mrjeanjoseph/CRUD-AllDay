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

        public void SaveMerchandise(Merchandise merchandise)
        {
            if (merchandise.Id == 0)
                context.Merchandise.Add(merchandise);
            else { 
                Merchandise dbEntry = context.Merchandise.Find(merchandise.Id);
                if(dbEntry != null )
                {
                    dbEntry.Name = merchandise.Name;
                    dbEntry.Description = merchandise.Description;
                    dbEntry.Price = merchandise.Price;
                    dbEntry.Category = merchandise.Category;
                    dbEntry.IsValid = merchandise.IsValid;
                }
            }
            context.SaveChanges();
        }
    }
}
