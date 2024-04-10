using System.Collections;
using System.Collections.Generic;

namespace EssentialFeatures.Models {

    public class ShoppingCart : IEnumerable<Product> {
        public List<Product> Products { get; set; }

        public IEnumerator<Product> GetEnumerator() {
            return Products.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
    }
    public class ShoppingCart_Old {
        public List<Product> Products { get; set; }
    }
}