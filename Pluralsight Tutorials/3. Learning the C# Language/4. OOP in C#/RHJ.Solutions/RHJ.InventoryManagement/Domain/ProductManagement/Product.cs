
using System.Text;

namespace RHJ.InventoryManagement.Domain
{
    public abstract partial class Product
    {
        private int id;
        private string name = string.Empty;
        private string? description;

        protected int MaxItemsInStock = 0;

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
        public int AmountInStock { get; protected set; }
        public bool IsBelowStockTreshold { get; protected set; }
        public Price Price { get; set; }

        public Product(int id, string name)
        {
            Id = id; Name = name;
        }
        public Product(int id) : this(id, string.Empty) { }

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

        public virtual void UseProduct(int items)
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

        public virtual void IncreaseStock() => AmountInStock++;


        public virtual void IncreaseStock(int amount)
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
            if (AmountInStock > StockTreshold)            
                IsBelowStockTreshold = false;            
        }

        public virtual void DecreaseStock(int items, string reason)
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

        public virtual string DisplayDetailsShort() => $"{Id}: {Name}\n{AmountInStock} items in stock";

        public virtual string DisplayDetailsFull(string extraDetails)
        {
            StringBuilder stringBuilder = new();
            // ToDo: Add price here too
            stringBuilder.Append($"{Id}: {Name}\n{Description}\n{Price}\n{AmountInStock} items in stock");
            stringBuilder.Append(extraDetails);
            if (IsBelowStockTreshold)
            {
                stringBuilder.Append("\n!!Stock Low!!");
            }
            return stringBuilder.ToString();
        }

        public virtual string DisplayDetailsFull()
        {
            StringBuilder stringBuilder = new();
            // ToDo: Add price here too
            stringBuilder.Append($"{Id}: {Name}\n{Description}\n{Price}\n{AmountInStock} items in stock");
            
            if (IsBelowStockTreshold)
            {
                stringBuilder.Append("\n!!Stock Low!!");
                
            }
            return stringBuilder.ToString();
            //return DisplayDetailsFull("");
        }
    }
}
