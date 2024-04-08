using System.Web.Mvc;

namespace IntroductionToMVC.Controllers {
    public class HomeController : Controller {
        // GET: Home
        public string Index() {
            return "<h1>Hello ASP.NET MVC 5</h1> - \nThis is Chapter 1, the introduction";
        }
    }
}