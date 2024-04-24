using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace SportsStore.Domain
{
    public class SportsStoreDbContext : DbContext
    {
        public DbSet<Merchandise> Merchandise_One { get; set; }
    }

    [Table("ProductVendor", Schema = "Purchasing")]
    public class AdvWorksDbContext : DbContext
    {
        public DbSet<ProductVendor> ProductVendor { get; set; }
    }
}
//No really_