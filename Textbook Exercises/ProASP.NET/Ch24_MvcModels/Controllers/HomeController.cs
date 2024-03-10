using Ch24_MvcModels.Models;
using System.Linq;
using System.Web.Mvc;

namespace Ch24_MvcModels.Controllers {
    public class HomeController : Controller {


        private readonly Person[] personData = {
            new Person {FirstName = "Louna", LastName = "Jean-Joseph", Role = Role.Admin },
            new Person {FirstName = "Raoul H.", LastName = "Jean-Joseph", Role = Role.Admin },
            new Person {FirstName = "Jovenel", LastName = "Moise", Role = Role.User },
            new Person {FirstName = "Eurie", LastName = "Latortue", Role = Role.User },
            new Person {FirstName = "Denver", LastName = "Mosque", Role = Role.Guest }
        };


        // GET: Home
        public ActionResult Index(int id) {
            Person personItem = personData.Where(p => p.PersonId == id).First();
            return View(personItem);
        }
    }
}