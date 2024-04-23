using System.Data.Entity;

namespace SportsStore.Domain
{
    public class EFDbContext : DbContext
    {
        public DbSet<Merchandise> Merchandises { get; set; }
    }
}
//No really