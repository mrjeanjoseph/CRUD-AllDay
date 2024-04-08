using System;
using System.Web.Mvc;

namespace Introduction.Controllers {
    public class HomeController : Controller {
        // GET: Home
        public ViewResult Index() {
            return View();
        }
    }
}