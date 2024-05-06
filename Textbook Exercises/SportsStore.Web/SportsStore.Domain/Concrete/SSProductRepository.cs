using System.Collections.Generic;

namespace SportsStore.Domain
{
    public class SSProductRepository : IProductRepository
    {
        private readonly SSDBContext _context = new SSDBContext();
        public IEnumerable<Product> Products
        {
            get { return _context.Products; }
        }

    }
}
