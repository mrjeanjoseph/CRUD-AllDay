using SportsStore.Domain;
using System.Web.Mvc;

namespace SportsStore.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _repository;

        public ProductController(IProductRepository productRepo)
        {
            _repository = productRepo;
        }
        // GET: Product
        public ViewResult ProductListing()
        {
            return View(_repository.Products);
        }
    }
}