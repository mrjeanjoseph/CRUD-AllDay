using System.Collections.Generic;

namespace SportsStore.Domain
{
    public interface IProductRepository
    {
        IEnumerable<Product> Products { get; }
    }
}
