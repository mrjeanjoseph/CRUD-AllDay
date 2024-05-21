using System.Text;

namespace RHJ.InventoryManagement.Domain
{
    internal class BoxedProduct : Product
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

        public string DisplayBoxedProductDetails()
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

        public void UseBoxedProduct(int items)
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
