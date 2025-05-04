using Microsoft.EntityFrameworkCore;

namespace AdventureWorks.Domain.DataAccessLayer;

public partial class AdWDbContext : DbContext {
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

}
