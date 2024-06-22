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
    }
}