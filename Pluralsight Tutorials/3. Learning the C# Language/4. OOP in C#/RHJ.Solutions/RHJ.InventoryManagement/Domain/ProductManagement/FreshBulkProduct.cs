namespace RHJ.InventoryManagement.Domain
{
    public class FreshBulkProduct : BoxedProduct
    {
        public FreshBulkProduct(int id, string name, string? description, 
            Price price, UnitType unitType, int maxAmtInStock, int amountPerBox) 
            : base(id, name, description, price, maxAmtInStock, amountPerBox) { }

        //public void UseFreshBoxedProduct(int item)
        //{
        //    UseBoxedProduct(3);
        //}
    }
}
