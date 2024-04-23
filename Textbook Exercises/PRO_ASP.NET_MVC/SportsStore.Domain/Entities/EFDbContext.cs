using System.Data.Entity;

namespace SportsStore.Domain
{
    public class EFDbContext : DbContext
    {
        public DbSet<Merchandise> Merchandise_One { get; set; }
    }
}
//No really_