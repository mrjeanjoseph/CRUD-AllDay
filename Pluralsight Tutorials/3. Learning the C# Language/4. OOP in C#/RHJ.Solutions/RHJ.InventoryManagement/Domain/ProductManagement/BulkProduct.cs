namespace RHJ.InventoryManagement.Domain
{
    public class BulkProduct : Product
    {
        public BulkProduct(int id, string name, string? description, Price price, UnitType unitType, int maxAmtInStock) : base(id, name, description, price, unitType, maxAmtInStock)
        {
        }
    }
}
