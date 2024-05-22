using System.Text;

namespace RHJ.InventoryManagement.Domain
{
    public class FreshProduct : Product
    {
        public DateTime ExpiryDateTime { get; set; }
        public string? StorageInstructions { get; set; }

        public FreshProduct(int id, string name, string? description, 
            Price price, UnitType unitType, int maxAmtInStock) 
            : base(id, name, description, price, unitType, maxAmtInStock) { }

        public override string DisplayDetailsFull()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("Fresh Product \n");

            stringBuilder.AppendLine($"{Id}: {Name}\n{Description}\n{Price}\n{AmountInStock} items in stock");
            if (IsBelowStockTreshold)
            {
                stringBuilder.AppendLine("\n!!Stock Low!!");
            }

            stringBuilder.AppendLine($"Storage instructions: {StorageInstructions}");
            stringBuilder.AppendLine($"Expiry data: " + ExpiryDateTime.ToShortDateString());

            return stringBuilder.ToString();
        }
    }
}
