using DetectingDeviceCapabilities.Models;
using System.Web.Mvc;

namespace DetectingDeviceCapabilities.Controllers
{
    public class HomeController : Controller
    {
        private readonly Programmer[] programmers =
        {
            new Programmer("Kervens", "Jean-Joseph", "Chief Operating Officer", "Saint Pete", "Florida", "Fullstack"),
            new Programmer("Denzel", "Paniague", "Software Manager", "Lakeland", "Florida", "C Plus Plus"),
            new Programmer("Nitaud", "Sage Paniague", "Engineer Director", "Roie Sex", "Haiti", "TSQL"),
            new Programmer("Elijah", "JeanJoseph", "Liason Officer", "Lamaine", "Texas", "Java"),
            new Programmer("Kalven", "Jouthe", "Contractor", "Broxton", "MA", "Frontend SE"),
        };

        public ActionResult Index()
        {
            return View(programmers);
        }

        public ActionResult BrowserDetail()
        {
            return View();
        }

        public ActionResult IndexTrace()
        {
            HttpContext.Trace.Write("HomeController", "IndexTrace Method Started");
            HttpContext.Trace.Write("HomeController", 
                string.Format("There are {0} programmers", programmers.Length));
            ActionResult result = View(programmers);
            HttpContext.Trace.Write("HomeController", "IndexTrace Method Completed");
            return result;
        }
    }
}