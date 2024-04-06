using System;
using System.Web.Mvc;

namespace Chapter2.Controllers {
    public class HomeController : Controller {
        // GET: Home
        public ViewResult Index() {
            int hour = DateTime.Now.Hour;
            ViewBag.Greeting = hour < 6 ? "It's too early Morning for this!"
                : hour < 12 ? "Good Morning"
                : hour < 16 ? "Good Afternoon"
                : hour < 20 ? "Good Evening"
                : "Time to get the party started!";
            ViewBag.Message = "We're going to have an exciting party tonight";
            return View();
        }

        public ActionResult RSVPForm() {
            return View();
        }
    }
}