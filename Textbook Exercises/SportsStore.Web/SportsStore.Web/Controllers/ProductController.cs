using SportsStore.Domain;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace SportsStore.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _repository;
        public int PageSize = 4;
        public ProductController(IProductRepository productRepo)
        {
            _repository = productRepo;
        }

        // GET: Product
        public ViewResult ProductListing(int page = 1)
        {
            return View(_repository.Products
                .OrderBy(p => p.ProductId)
                .Skip((page - 1) * PageSize)
                .Take(PageSize));
        }
    }
}