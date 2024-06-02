using Chapter24.ModelBinding;
using System.Linq;
using System.Web.Mvc;

namespace PartFour.AdvConcepts.Areas.Chapter24.Controllers
{
    public class PersonController : Controller
    {
        private readonly Person[] personData =
{
            new Person { PersonId = 105, FirstName = "Kervens", LastName = "Jean-Joseph", Role = Role.Admin },
            new Person { PersonId = 105, FirstName = "Chistine", LastName = "Fleurant", Role = Role.User },
            new Person { PersonId = 105, FirstName = "Nitaud", LastName = "Paniague", Role = Role.User },
            new Person { PersonId = 105, FirstName = "Jean-Jacques", LastName = "LeBlanc", Role = Role.Guest },
            new Person { PersonId = 105, FirstName = "Denzel", LastName = "Dure", Role = Role.User },
        };
        // GET: Chapter24/Person
        public ActionResult Index(int id)
        {
            Person dataItem = personData.Where(p => p.PersonId == id).First();
            return View(dataItem);
        }
    }
}