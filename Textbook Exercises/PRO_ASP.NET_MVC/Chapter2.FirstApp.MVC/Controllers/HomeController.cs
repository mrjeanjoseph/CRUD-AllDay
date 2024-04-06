using System.Web.Mvc;

namespace Chapter2.FirstApp.MVC.Controllers {
    public class HomeController : Controller {
        // GET: Home
        public string Index() {
            return "Hello ASP.NET MVC 5";
        }
    }
}