using System.Web.Mvc;

namespace Chapter14.OverviewOfMVC.Controllers
{
    public class HomeController : Controller
    {
        // GET: Chapter14/Home
        public ActionResult Index()
        {
            int firstVal = 10;
            int secondVal = 5;
            int result = firstVal / secondVal;

            ViewBag.Message = "Welcome to ASP.NET MVC!";

            return View(result);
        }
    }
}