namespace RHJ.InventoryManagement.Domain
{
    public class FreshProduct : Product
    {
        public DateTime ExpiryDateTime { get; set; }
        public string? StorageInstructions { get; set; }

        public FreshProduct(int id, string name, string? description, Price price, UnitType unitType, int maxAmtInStock) : base(id, name, description, price, unitType, maxAmtInStock)
        {
        }
    }
}
