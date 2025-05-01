using DPE.DomainService.Models;
using DPE.DomainService.UserData;
using System.Collections.Generic;
using System.Linq;

namespace DPE.DomainService.DataAccess
{
    public class ProductData : IProductData
    {
        private readonly ISqlDataAccess _sqlData;

        public ProductData(ISqlDataAccess sqlData)
        {
            _sqlData = sqlData;
        }

        public List<ProductModel> GetProducts()
        {
            return _sqlData.LoadData<ProductModel, dynamic>("dbo.spProductGetAll", new { }, "CCMSConn");
        }

        public ProductModel GetProductById(int productId)
        {

            return _sqlData.LoadData<ProductModel, dynamic>("dbo.spProductGetById",
                new { id = productId }, "CCMSConn").FirstOrDefault();

        }
    }
}
