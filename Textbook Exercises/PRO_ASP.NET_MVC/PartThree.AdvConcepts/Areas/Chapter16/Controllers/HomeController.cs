using System.Web.Mvc;

namespace Chapter16.URLsAndRoutes.Controllers
{
    public class HomeController : Controller
    {
        // GET: Chapter15/Home
        public ActionResult Index()
        {
            ViewBag.AreaName = "URLsAndRoutes";
            ViewBag.ControllerName = "Home";
            ViewBag.ActionName = "Index";

            return View("ActionName");
        }

        public ActionResult CustomVariable(string id = "DefaultId")
        {
            ViewBag.AreaName = "URLsAndRoutes";
            ViewBag.ControllerName = "Home";
            ViewBag.ActionName = "Index";
            //ViewBag.CustomVariable = id ?? "<No Value Provided>";
            ViewBag.CustomVariable = id;

            return View();
        }
        public ActionResult About()
        {
            ViewBag.AreaName = "URLsAndRoutes";
            ViewBag.ControllerName = "Home";
            ViewBag.ActionName = "Index";
            ViewBag.CustomVariable = RouteData.Values["id"];

            return View();
        }
    }
}