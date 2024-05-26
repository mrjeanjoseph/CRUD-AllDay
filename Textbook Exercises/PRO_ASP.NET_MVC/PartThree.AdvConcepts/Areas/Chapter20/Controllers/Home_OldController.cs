using System;
using System.Web.Mvc;

namespace Chapter20.RazorPagesAndViewEngine.Controllers
{
    public class HomeOldController : Controller
    {
        // GET: Chapter20/Home
        public ActionResult Index()
        {
            ViewBag.Message = "Hello C#/.NET programmers";
            ViewBag.CurrentTime = DateTime.Now.ToShortDateString();
            return View("DebugData");
        }

        public ActionResult ListAllData()
        {
            return View();
        }
    }
}