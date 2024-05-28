using Chapter22.TemplatedHelperMethods.Models;
using System.Web.Mvc;

namespace Chapter22.TemplatedHelperMethods.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Plants = new string[] { "Snake Plants", "Mother-in-law's tongue", "African Spear Plant" };
            ViewBag.Cities = new string[] { "Raleigh", "Durham", "Chapel Hill" };
            string message = "This is an HTML element: <input> and this one <SnakePlants>";

            return View((object)message);
        }

        public ActionResult CreatePerson()
        {
            return View(new Person());
        }

        [HttpPost]
        public ActionResult CreatePerson(Person person)
        {
            return View("DisplayPerson",person);
        }
    }
}