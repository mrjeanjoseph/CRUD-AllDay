using System.Collections.Generic;

namespace SportsStore.Domain
{
    public interface IProductVendorRepo {
        IEnumerable<ProductVendor> ProductVendor { get; }
    }
}
