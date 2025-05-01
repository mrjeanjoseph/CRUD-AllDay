using DPE.DomainService.Models;
using System.Collections.Generic;

namespace DPE.DomainService.DataAccess
{
    public interface IProductData
    {
        ProductModel GetProductById(int productId);
        List<ProductModel> GetProducts();
    }
}