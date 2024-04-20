using SportStore.Domain.Abstract;
using System.Web.Mvc;

namespace SportStore.Controllers {
    public class CommodityController : Controller {
        private readonly ICommodityRepository _repository;

        public CommodityController(ICommodityRepository repository) {
            _repository = repository;
        }

        public ActionResult CommodityList() {
            return View(_repository.Commodities);
        }
    }
}