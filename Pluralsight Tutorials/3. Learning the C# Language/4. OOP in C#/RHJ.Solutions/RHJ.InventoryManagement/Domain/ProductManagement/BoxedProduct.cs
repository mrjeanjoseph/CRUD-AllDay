using System.Text;

namespace RHJ.InventoryManagement.Domain
{
    public class BoxedProduct : Product
    {
        private int amountPerBox;

        public int AmountPerBox
        {
            get { return amountPerBox; }
            set { amountPerBox = value; }
        }
        public BoxedProduct(int id, string name, string? description, 
            Price price, int maxAmtInStock, int amountPerBox) 
            : base(id, name, description, price, UnitType.PerBox, maxAmtInStock)
        {
            AmountPerBox = amountPerBox;
        }

        public override string DisplayDetailsFull()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append("Boxed Product \n");

            stringBuilder.Append($"{Id}: {Name}\n{Description}\n{Price}\n{AmountInStock} items in stock");
            if (IsBelowStockTreshold)
            {
                stringBuilder.Append("\n!!Stock Low!!");
            }
            return stringBuilder.ToString();
        }

        public override void UseProduct(int items)
        {
            int smallestMultiple = 0;
            int batchSize;

            while (true)
            {
                smallestMultiple++;
                if (smallestMultiple * amountPerBox > items)
                {
                    batchSize = smallestMultiple * amountPerBox;
                    break;
                }
            }
            base.UseProduct(batchSize);
        }

        public override void IncreaseStock() => AmountInStock += AmountPerBox;


        public override void IncreaseStock(int amount)
        {
            int newStock = AmountInStock + amount * amountPerBox;
            if (newStock <= MaxItemsInStock)
                AmountInStock += amount * amountPerBox;
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

        //Will no longer us
        public void UseBoxedProduct_old(int items)
        {
            int smallestMultiple = 0;
            int batchSize;

            while (true)
            {
                smallestMultiple++;
                if (smallestMultiple * amountPerBox > items)
                {
                    batchSize = smallestMultiple * amountPerBox;
                    break;
                }
            }
            UseProduct(batchSize);
        }
    }
}
