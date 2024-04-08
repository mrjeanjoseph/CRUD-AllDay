using System.Web.Mvc;

namespace Chapter1.Introduction.Controllers {
    public class HomeController : Controller {
        // GET: Chapter1/Home
        public string Index() {
            return "<h1>Hello ASP.NET MVC 5</h1> - \n<em>This is Chapter 1, the Introduction</em>";
        }
    }
}