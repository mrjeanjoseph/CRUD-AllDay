using System.Data.Entity;

namespace SportsStore.Domain
{
    public class SSDBContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
    }
}
