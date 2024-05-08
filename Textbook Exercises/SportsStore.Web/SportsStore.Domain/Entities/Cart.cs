using System.Collections.Generic;
using System.Linq;

namespace SportsStore.Domain
{
    public class Cart
    {
        private readonly List<CartLine> lineCollection = new List<CartLine>();

        public void AddItem(Product product, int quanity)
        {
            CartLine line = lineCollection
                .Where(l => l.Product.ProductId == product.ProductId)
                .FirstOrDefault();

            if (line == null) lineCollection.Add(new CartLine { Product = product, Quantity = quanity });
            else line.Quantity += quanity;            

        }

        public void RemoveItem(Product product) => lineCollection.RemoveAll(l => l.Product.ProductId == product.ProductId);        

        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(e => e.Product.Price * e.Quantity);
        }

        public void Clear() => lineCollection.Clear();

        public IEnumerable<CartLine> Lines { get { return lineCollection; } }
    }

    public class CartLine
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
