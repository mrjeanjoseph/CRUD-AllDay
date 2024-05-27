using System;
using System.Web.Mvc;

namespace Chapter20.RazorPagesAndViewEngine.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            string[] items = { "Snake Plants", "Banana Plants", "Shovel", "Mulch" };
            return View(items);
        }

        public ActionResult List()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult Time()
        {
            return PartialView(DateTime.Now);
        }
    }
}