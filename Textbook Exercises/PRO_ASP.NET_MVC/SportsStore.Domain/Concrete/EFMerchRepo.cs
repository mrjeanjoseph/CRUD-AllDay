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

        public void SaveMerchandise(Merchandise merch)
        {
            if (merch.Id == 0)
                context.Merchandise.Add(merch);
            else { 
                Merchandise dbEntry = context.Merchandise.Find(merch.Id);
                if(dbEntry != null )
                {
                    dbEntry.Name = merch.Name;
                    dbEntry.Description = merch.Description;
                    dbEntry.Price = merch.Price;
                    dbEntry.Category = merch.Category;
                    dbEntry.IsValid = merch.IsValid;
                    dbEntry.ImageData = merch.ImageData;
                    dbEntry.ImageMimeType = merch.ImageMimeType;
                }
            }
            context.SaveChanges();
        }

        public Merchandise DeleteMerchandise(int id)
        {
            Merchandise dbEntry = context.Merchandise.Find(id);
            if(dbEntry != null)
            {
                context.Merchandise.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
