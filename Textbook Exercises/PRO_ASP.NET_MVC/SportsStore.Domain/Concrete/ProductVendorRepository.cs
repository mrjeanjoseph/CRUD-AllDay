using System.Collections.Generic;

namespace SportsStore.Domain
{
    public class ProductVendorRepository : IProductVendorRepo
    {
        private readonly AdvWorksDbContext AdvWorksDbContext = new AdvWorksDbContext();
        public IEnumerable<ProductVendor> ProductVendor
        {
            get { return AdvWorksDbContext.ProductVendor; }
        }
    }
}
