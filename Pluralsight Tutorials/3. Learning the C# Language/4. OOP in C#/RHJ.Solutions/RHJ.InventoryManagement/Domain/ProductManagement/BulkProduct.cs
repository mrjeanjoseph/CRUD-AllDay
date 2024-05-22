namespace RHJ.InventoryManagement.Domain
{
    public class BulkProduct : Product
    {
        public BulkProduct(int id, string name, string? description, 
            Price price, int maxAmtInStock) 
            : base(id, name, description, price, maxAmtInStock)
        {
        }
    }
}
