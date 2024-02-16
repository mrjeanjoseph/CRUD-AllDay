using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RenderViews.Controllers {
    public class HomeController : Controller {
        // GET: Home
        public ActionResult Index() {
            ViewBag.Message = "Hello MVC";
            ViewBag.Time = DateTime.Now.ToShortTimeString();
            return View("DebugData");
        }

        public ActionResult About() {
            return View();
        }
    }
}