using SportsStore.Domain.Entities;
using System.Linq;

namespace SportsStore.Domain.Abstract {
    internal interface IProductsRepository {

        IQueryable<Product> Products { get; }

        void SaveProduct(Product product);

        Product DeleteProduct(int productId);
    }
}
