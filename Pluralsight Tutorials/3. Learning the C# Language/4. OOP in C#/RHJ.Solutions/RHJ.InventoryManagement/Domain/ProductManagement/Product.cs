
using System.Text;

namespace RHJ.InventoryManagement.Domain
{
    public partial class Product
    {
        private int id;
        private string name = string.Empty;
        private string? description;

        private int MaxItemsInStock = 0;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value.Length > 50 ? value[..50] : value; }
        }

        public string? Description
        {
            get { return description; }
            set
            {
                description = value == null ? description = string.Empty
                    : description = value.Length > 250 ? value[..250] : value;
            }
        }

        public UnitType UnitType { get; set; }
        public int AmountInStock { get; private set; }
        public bool IsBelowStockThreshold { get; private set; }
        public Price Price { get; set; }

        public Product(int id) : this(id, string.Empty) { }

        public Product(int id, string name)
        {
            Id = id; Name = name;
        }
        public Product(int id, string name, string? description, Price price, UnitType unitType, int maxAmtInStock)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            UnitType = unitType;

            MaxItemsInStock = maxAmtInStock;

            UpdateLowStock();
        }

        public void UseProduct(int items)
        {
            if (items <= AmountInStock)
            {
                //use the item
                AmountInStock -= items;

                UpdateLowStock();
                Log($"Amount in stock updated. Now {AmountInStock} items in stock.");
            }
            else
            {
                Log($"Not enough items on stock for {CreateSimpleProductRepresentation()}." +
                    $" {AmountInStock} available but {items} requested");
            }

        }

        public void IncreaseStock() => AmountInStock++;
        

        public void IncreaseStock(int amount)
        {
            int newStock = AmountInStock + amount;
            if (newStock <= AmountInStock)
                AmountInStock += amount;
            else
            {
                AmountInStock = MaxItemsInStock; // We only store the possible items,
                //overstock is not stored.
                Log($"{CreateSimpleProductRepresentation} stock overflow. " +
                    $"{newStock - AmountInStock} item(s) ordered that could not be stored");
            }
            if (AmountInStock > 10)            
                IsBelowStockThreshold = false;            
        }

        public void DecreaseStock(int items, string reason)
        {
            if (items <= AmountInStock)
            {
                AmountInStock -= items;

            }
            else
            {
                AmountInStock = 0;
            }
            UpdateLowStock();
            Log(reason);
        }

        public string DisplayDetailsShort() => $"{Id}: {Name}\n{AmountInStock} items in stock";

        public string DisplayDetailsFull(string extraDetails)
        {
            StringBuilder stringBuilder = new();
            // ToDo: Add price here too
            stringBuilder.Append($"{Id}: {Name}\n{Description}\n{Price}\n{AmountInStock} items in stock");
            stringBuilder.Append(extraDetails);
            if (IsBelowStockThreshold)
            {
                stringBuilder.Append("\n!!Stock Low!!");
            }
            return stringBuilder.ToString();
        }

        public string DisplayDetailsFull()
        {
            StringBuilder stringBuilder = new();
            // ToDo: Add price here too
            stringBuilder.Append($"{Id}: {Name}\n{Description}\n{Price}\n{AmountInStock} items in stock");
            
            if (IsBelowStockThreshold)
            {
                stringBuilder.Append("\n!!Stock Low!!");
                
            }
            return stringBuilder.ToString();
            //return DisplayDetailsFull("");
        }
    }
}
