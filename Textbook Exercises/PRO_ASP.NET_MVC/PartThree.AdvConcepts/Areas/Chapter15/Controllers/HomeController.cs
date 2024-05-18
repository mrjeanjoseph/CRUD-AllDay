using System.Web.Mvc;

namespace Chapter15.URLsAndRoutes.Controllers
{
    public class HomeController : Controller
    {
        // GET: Chapter15/Home
        public ActionResult Index()
        {
            ViewBag.Controller = "Home";
            ViewBag.Action = "Index";

            return View("ActionName");
        }

        public ActionResult CustomVariable(string id = "DefaultId")
        {
            ViewBag.Controller = "Home";
            ViewBag.Action = "CustomVariable";
            //ViewBag.CustomVariable = id ?? "<No Value Provided>";
            ViewBag.CustomVariable = id;

            return View();
        }
        public ActionResult About()
        {
            ViewBag.Controller = "Home";
            ViewBag.Action = "About";
            ViewBag.CustomVariable = RouteData.Values["id"];

            return View();
        }
    }
}