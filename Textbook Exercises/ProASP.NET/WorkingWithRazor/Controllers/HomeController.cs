using System;
using System.Web.Mvc;

namespace WorkingWithRazor.Controllers {
    public class HomeController : Controller {
        // GET: Home
        public ActionResult Index() {

            string[] names = { "Apples", "Oranges", "Mango", "Guayava", "Passion Fruit" };
            return View(names);
        }

        public ActionResult List() {
            return View();
        }

        [ChildActionOnly]
        public ActionResult OurTime() {
            return PartialView(DateTime.Now);
        }


        public ActionResult IndexOld() {
            ViewBag.Message = "Hello MVC";
            ViewBag.Time = DateTime.Now.ToShortTimeString();

        }
    }