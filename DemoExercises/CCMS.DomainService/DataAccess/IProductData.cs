using CCMS.DomainService.Models;
using System.Collections.Generic;

namespace CCMS.DomainService.DataAccess
{
    public interface IProductData
    {
        ProductModel GetProductById(int productId);
        List<ProductModel> GetProducts();
    }
}