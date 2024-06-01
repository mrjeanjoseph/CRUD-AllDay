using Chapter23.AjaxHelperMethods.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Chapter23.AjaxHelperMethods.Controllers
{
    public class PeopleController : Controller
    {
        private readonly Person[] personData =
        {
            new Person { FirstName = "Kervens", LastName = "Jean-Joseph", Role = Role.Admin },
            new Person { FirstName = "Chistine", LastName = "Fleurant", Role = Role.User },
            new Person { FirstName = "Nitaud", LastName = "Paniague", Role = Role.User },
            new Person { FirstName = "Jean-Jacques", LastName = "LeBlanc", Role = Role.Guest },
            new Person { FirstName = "Denzel", LastName = "Dure", Role = Role.User },
        };
        // GET: Chapter23/People
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetPeople()
        {
            return View(personData);
        }

        [HttpPost]
        public ActionResult GetPeople(string selectedRole)
        {
            if (selectedRole == null || selectedRole == "All")            
                return View(personData);            
            else
            {
                Role selected = (Role)Enum.Parse(typeof(Role), selectedRole);
                return View(personData.Where(p => p.Role == selected));
            }
        }
    }
}