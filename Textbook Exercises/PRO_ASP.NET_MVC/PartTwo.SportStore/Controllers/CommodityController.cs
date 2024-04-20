using SportStore.Domain;
using System.Web.Mvc;

namespace SportStore.Controllers {
    public class CommodityController : Controller {
        private readonly ICommodityRepository _repository;

        public CommodityController(ICommodityRepository repository) {
            _repository = repository;
        }

        public ActionResult List() {
            return View(_repository.Commodities);
        }
    }
}