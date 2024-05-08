using SportsStore.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SportsStore.Web.Controllers
{
    public class NavController : Controller
    {
        private readonly IProductRepository _productRepo;

        public NavController(IProductRepository repoParam)
        {
            _productRepo = repoParam;
        }
        // GET: Nav
        public PartialViewResult Menu(string category = null)
        {
            ViewBag.SelectedCategory = category;

            IEnumerable<string> categories = _productRepo.Products
                                .Select(c => c.Category)
                                .Distinct()
                                .OrderBy(c => c);
            return PartialView(categories);
        }
    }
}