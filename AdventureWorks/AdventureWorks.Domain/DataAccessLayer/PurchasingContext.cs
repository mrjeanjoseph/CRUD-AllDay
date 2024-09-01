using Microsoft.EntityFrameworkCore;

namespace AdventureWorks.Domain.DataAccessLayer;

public partial class AdWDbContext : DbContext {
    public virtual DbSet<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }
    public virtual DbSet<PurchaseOrderHeader> PurchaseOrderHeaders { get; set; }
    public virtual DbSet<ShipMethod> ShipMethods { get; set; }
    public virtual DbSet<VVendorWithAddress> VVendorWithAddresses { get; set; }
    public virtual DbSet<VVendorWithContact> VVendorWithContacts { get; set; }

}
