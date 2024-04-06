using System;
using System.Web.Mvc;

namespace Chapter2.Controllers {
    public class HomeController : Controller {
        // GET: Home
        public ViewResult Index() {

            int hour = DateTime.Now.Hour;
            ViewBag.Greeting = hour < 12 ? "Good Morning" : "Good Afternoon";
            return View();
        }
    }
}