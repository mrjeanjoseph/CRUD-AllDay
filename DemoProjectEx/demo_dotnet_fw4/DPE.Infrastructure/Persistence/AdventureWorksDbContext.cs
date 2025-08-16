using DPE.Infrastructure.Entities;
using System.Data.Entity;

namespace DPE.Infrastructure.Persistence
{
    public class AdventureWorksDbContext : DbContext
    {
        public AdventureWorksDbContext() : base("name=AdventureWorksConnection")
        {
            // Optional: Configuration settings
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<PersonEntity> Persons { get; set; }

        // Add other DbSets as needed
        // public DbSet<ClientEntity> Clients { get; set; }
        // public DbSet<RequestEntity> Requests { get; set; }
    }
}