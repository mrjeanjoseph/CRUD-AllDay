using System.Web.Mvc;

namespace Chapter20.RazorViewEngine.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            string[] items = { "Snake Plants", "Banana Plants", "Shovel", "Mulch" };
            return View(items);
        }
    }
}