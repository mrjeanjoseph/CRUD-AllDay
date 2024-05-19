using System.Web.Mvc;

namespace Chapter16.URLsAndRoutes.Controllers
{
    public class HomeController : Controller
    {
        // GET: Chapter15/Home
        public ActionResult Index()
        {
            ViewBag.AreaName = "URLs and Routes";
            ViewBag.ControllerName = "Home";
            ViewBag.ActionName = "Index";

            return View("ActionName");
        }

        public ActionResult CustomVariable(string id = "DefaultId")
        {
            ViewBag.AreaName = "URLs and Routes";
            ViewBag.ControllerName = "Home";
            ViewBag.ActionName = "Custom Variable";
            //ViewBag.CustomVariable = id ?? "<No Value Provided>";
            ViewBag.CustomVariable = id;

            return View("ActionName");
        }
        public ActionResult About()
        {
            ViewBag.AreaName = "URLs and Routes";
            ViewBag.ControllerName = "Home";
            ViewBag.ActionName = "About";
            ViewBag.CustomVariable = RouteData.Values["id"];

            return View();
        }

        public ActionResult MyActionMethod()
        {
            string myActionUrl = Url.Action("Index", new { id = "MyId" });
            string myRouteRul = Url.RouteUrl(new { controller = "Home", action = "Index" });
            //... Do something cool with the urls
            return View();
        }
    }
}