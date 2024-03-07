using Ch21_HelperMethods.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<Person> GetData(string selectedRole) {
            IEnumerable<Person> data = personData;
            if (selectedRole != "All") {
                Role selected = (Role)Enum.Parse(typeof(Role), selectedRole);
                data = personData.Where(p => p.Role == selected);
            }
            return data;
        }

        public ActionResult GetPeopleData(string selectedRole = "All") {

            
            IEnumerable<Person> data = personData;
            if(selectedRole != "All") {
                Role selected = (Role)Enum.Parse(typeof(Role), selectedRole);
                data = personData.Where(p => p.Role == selected);
            }
            if(Request.IsAjaxRequest()) {
                var formattedData = data.Select(p => new {
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Role = Enum.GetName(typeof(Role), p.Role)
                });
                return Json(formattedData, JsonRequestBehavior.AllowGet );
            } else {
                return PartialView(data);
            }

        }

        public JsonResult GetPeopleDataJson2(string selectedRole = "All") {
            //IEnumerable<Person> data = GetData(selectedRole);

            var data = GetData(selectedRole).Select(p => new {
                FirstName = p.FirstName,
                LastName = p.LastName,
                Role = Enum.GetName(typeof(Role), p.Role)
            });
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult GetPeopleData3(string selectedRole = "All") {
            return PartialView(GetData(selectedRole));
        }

        public ActionResult GetPeople(string selectedRole = "All") {
            return View((object)selectedRole);
        }

        #region Old code
        public ActionResult GetPeople2(string selectedRole = "All") {
            return View((object)selectedRole);
        }

        public PartialViewResult GetPeopleData2(string selectedRole = "All") {
            IEnumerable<Person> data = personData;
            if (selectedRole != "All") {
                Role selected = (Role)Enum.Parse(typeof(Role), selectedRole);
                data = personData.Where(p => p.Role == selected);
            }

            return PartialView(data);
        }


        public ActionResult GetPeople2() {
            return View(personData);
        }

        [HttpPost]
        public ActionResult GetPeople3(string selectedRole) {
            if (selectedRole == null || selectedRole == "All") {
                return View(personData);
            } else {
                Role selected = (Role)Enum.Parse(typeof(Role), selectedRole);
                return View(personData.Where(p => p.Role == selected));
            }
        }


        #endregion
    }
}