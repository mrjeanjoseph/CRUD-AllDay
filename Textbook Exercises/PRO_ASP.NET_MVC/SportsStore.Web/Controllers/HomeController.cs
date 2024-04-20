using System.Web.Mvc;

namespace SportsStore.Web.Controllers {
    public class HomeController : Controller {
        // GET: Home
        public ActionResult Index() {
            return View();
        }

        public ActionResult List() {
            return View();
        }
    }
}