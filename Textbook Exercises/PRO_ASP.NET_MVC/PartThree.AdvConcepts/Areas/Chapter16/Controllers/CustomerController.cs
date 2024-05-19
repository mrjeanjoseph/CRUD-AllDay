using System.Web.Mvc;

namespace Chapter16.URLsAndRoutes.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Chapter15/Home
        public ActionResult Index()
        {
            ViewBag.AreaName = "URLs and Routes";
            ViewBag.ControllerName = "Customer";
            ViewBag.ActionName = "Index";

            return View("ActionName");
        }

        [Route("~/TestPage")]
        public ActionResult TestMethod()
        {
            ViewBag.AreaName = "URLs and Routes";
            ViewBag.ControllerName = "Customer";
            ViewBag.ActionName = "Index";

            return View("ActionName");
        }

        public ActionResult CustomVariable(string customerId = "DefaultId")
        {
            ViewBag.AreaName = "URLs and Routes";
            ViewBag.ControllerName = "Customer";
            ViewBag.ActionName = "CustomVariable";
            //ViewBag.CustomVariable = id ?? "<No Value Provided>";
            ViewBag.CustomVariable = customerId;

            return View("ActionName");
        }
        public ActionResult About()
        {
            ViewBag.AreaName = "URLs and Routes";
            ViewBag.ControllerName = "Customer";
            ViewBag.ActionName = "About";
            ViewBag.CustomVariable = RouteData.Values["id"];

            return View();
        }
    }
}