namespace RHJ.InventoryManagement.Domain
{
    public class RegularProduct : Product
    {
        public RegularProduct(int id, string name, string? description, 
            Price price, UnitType unitType, int maxAmtInStock, int amountPerBox) 
            : base(id, name, description, price, unitType, maxAmtInStock) { }
    }
}
