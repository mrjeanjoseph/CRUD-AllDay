using Ch21_HelperMethods.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ch21_HelperMethods.Controllers {
    public class PeopleController : Controller {

        private readonly Person[] personData = {
            new Person {FirstName = "Louna", LastName = "Jean-Joseph", Role = Role.Admin },
            new Person {FirstName = "Raoul H.", LastName = "Jean-Joseph", Role = Role.Admin },
            new Person {FirstName = "Jovenel", LastName = "Moise", Role = Role.User },
            new Person {FirstName = "Eurie", LastName = "Latortue", Role = Role.User },
            new Person {FirstName = "Denver", LastName = "Mosque", Role = Role.Guest }
        };

        public ActionResult Index() {
            return View();
        }

        public ActionResult GetPeople() {
            return View(personData);
        }

        [HttpPost]
        public ActionResult GetPeople(string selectedRole) {
            if(selectedRole == null || selectedRole == "All") {
                return View(personData);
            }
            else {
                Role selected = (Role)Enum.Parse(typeof(Role), selectedRole);
                return View(personData.Where(p => p.Role == selected));
            }
        }
    }
}