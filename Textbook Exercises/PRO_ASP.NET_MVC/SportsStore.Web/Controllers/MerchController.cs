using SportsStore.Domain;
using System.Web.Mvc;

namespace SportsStore.Web.Controllers {
    public class MerchController : Controller {
        private IMerchRepo _repository;

        public MerchController(IMerchRepo merchRepo) {
            _repository = merchRepo;
        }

        public ActionResult List() {
            return View(_repository.Merchandises);
        }
    }
}