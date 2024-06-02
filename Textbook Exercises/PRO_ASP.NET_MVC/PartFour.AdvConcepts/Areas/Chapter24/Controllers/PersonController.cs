using Chapter24.ModelBinding.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Chapter24.ModelBinding.Controllers
{
    public class PersonController : Controller
    {
        private readonly Person[] personData = {

            new Person { PersonId = 105, FirstName = "Kervens", LastName = "Jean-Joseph", Role = Role.Admin },
            new Person { PersonId = 110, FirstName = "Chistine", LastName = "Fleurant", Role = Role.User },
            new Person { PersonId = 115, FirstName = "Nitaud", LastName = "Paniague", Role = Role.User },
            new Person { PersonId = 120, FirstName = "Jean-Jacques", LastName = "LeBlanc", Role = Role.Guest },
            new Person { PersonId = 125, FirstName = "Denzel", LastName = "Dure", Role = Role.User },
        };
        
        public ActionResult Index(int id = 105)
        {
            Person dataItem = personData.Where(p => p.PersonId == id).First();
            return View(dataItem);
        }

        public ActionResult CreatePerson()
        {
            return View(new Person());
        }

        [HttpPost]
        public ActionResult CreatePerson(Person model)
        {
            return View("Index", model);
        }

        public ActionResult DisplaySummary(
            [Bind(Prefix ="HomeAddress",Exclude = "Country")] AddressSummary model)
        {
            return View(model);
        }

        public ActionResult Address(IList<AddressSummary> addresses)
        {
            addresses = addresses ?? new List<AddressSummary>();
            return View(addresses);
        }
    }
}