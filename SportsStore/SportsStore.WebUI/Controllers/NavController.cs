using SportsStore.Domain.Abstract;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SportsStore.WebUI.Controllers {
    public class NavController : Controller {

        private readonly IProductRepository _repository;

        public NavController(IProductRepository repository) {
            _repository = repository;
        }

        public PartialViewResult Menu(string category = null) {
            ViewBag.SelectedCategory = category;

            IEnumerable<string> categories = _repository.Products
                .Select(c => c.Category)
                .Distinct()
                .OrderBy(c => c);

            return PartialView("MenuFlex", categories);
        }
        public PartialViewResult Menu_Old(string category = null, bool horizontalLayout = false) {
            ViewBag.SelectedCategory = category;

            IEnumerable<string> categories = _repository.Products
                .Select(c => c.Category)
                .Distinct()
                .OrderBy(c => c);
            string viewName = horizontalLayout ? "MenuHorizontal" : "Menu";

            return PartialView(viewName, categories);
        }
    }
}
