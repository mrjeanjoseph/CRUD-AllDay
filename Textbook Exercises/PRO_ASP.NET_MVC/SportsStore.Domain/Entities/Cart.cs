using System.Collections.Generic;
using System.Linq;

namespace SportsStore.Domain
{
    public class Cart
    {
        private readonly List<CartLine> lineCollection = new List<CartLine>();

        public void AddItem(Merchandise merch, int quanity)
        {
            CartLine line = lineCollection
                .Where(m => m.Merchandise.Id == merch.Id)
                .FirstOrDefault();

            if (line == null)
                lineCollection.Add(new CartLine { Merchandise = merch, Quantity = quanity });
            else
                line.Quantity += quanity;
        }

        public void RemoveLine(Merchandise merch) => lineCollection.RemoveAll(l => l.Merchandise.Id == merch.Id);

        public decimal ComputeTotalValue() => lineCollection.Sum(m => m.Merchandise.Price * m.Quantity);        

        public void Clear() => lineCollection.Clear();        

        public IEnumerable<CartLine> Lines { get { return lineCollection; } }
    }

    public class CartLine
    {
        public Merchandise Merchandise { get; set; }
        public int Quantity { get; set; }
    }
}