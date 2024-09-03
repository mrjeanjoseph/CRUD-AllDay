using SportsStore.Domain.Abstract;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SportsStore.WebUI.Controllers {
    public class NavController : Controller {

        private IProductsRepository repository;

        public NavController(IProductsRepository repository) {
            this.repository = repository;
        }

        public PartialViewResult Menu(string category = null) {

            ViewBag.selectedcategory = category;

            IEnumerable<string> _m = repository.Products
                .Select(p => p.Category).Distinct().OrderBy(p => p);

            return PartialView(_m);
        }
    }

}