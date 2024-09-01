namespace AdventureWorks.Domain.Models;

public partial class ProductVendor {
    public int ProductId { get; set; }

    public int StandardPrice { get; set; }

    public DateTime LastReceiptDate { get; set; }
}
