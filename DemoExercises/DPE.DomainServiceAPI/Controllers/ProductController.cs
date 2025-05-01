using DPE.DomainService.DataAccess;
using DPE.DomainService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DPE.DomainServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Cashier")]
    public class ProductController : ControllerBase
    {
        private readonly IProductData _product;

        public ProductController(IProductData product)
        {
            _product = product;
        }

        [HttpGet]
        public List<ProductModel> Get()
        {
            return _product.GetProducts();
        }
    }
}
