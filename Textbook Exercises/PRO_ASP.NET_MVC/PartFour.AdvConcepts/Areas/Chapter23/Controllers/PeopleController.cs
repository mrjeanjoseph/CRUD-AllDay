using Chapter23.AjaxHelperMethods.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public ActionResult Index()
        {
            return View();
        }

        #region Using the Json Way
        private IEnumerable<Person> GetData(string selectedRole)
        {
            IEnumerable<Person> data = personData;

            if (selectedRole != "All")
            {
                Role selected = (Role)Enum.Parse(typeof(Role), selectedRole);
                data = personData.Where(p => p.Role == selected);
            }
            return data;
        }

        public JsonResult GetPeopleDataJson(string selectedRole = "All")
        {
            //IEnumerable<Person> data = GetData(selectedRole);
            var data = GetData(selectedRole).Select(p => new
            {
                p.FirstName,
                p.LastName,
                Role = Enum.GetName(typeof(Role), p.Role)
            });
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult GetPeopleDataJsonWay(string selectedRole = "All")
        {
            return PartialView("_GetPeopleData", GetData(selectedRole));
        }
        #endregion

        #region Fetching record the Ajax way        
        public PartialViewResult GetPeopleDataAjaxWay(string selectedRole = "All")
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            IEnumerable<Person> data = personData;

            if (selectedRole != "All")
            {
                Role selected = (Role)Enum.Parse(typeof(Role), selectedRole);
                data = personData.Where(p => p.Role == selected);
            }

            stopwatch.Stop();
            ViewBag.ServerProcessingTime = stopwatch.ElapsedMilliseconds;

            return PartialView("_GetPeopleData", data);
        }

        public ActionResult GetPeople(string selectedRole = "All")
        {
            return View((object)selectedRole);
        }
        #endregion

        #region Regular way of getting people data
        public ActionResult GetPeopleRegularWay()
        {
            return View(personData);
        }

        [HttpPost]
        public ActionResult GetPeopleRegularWay(string selectedRole)
        {
            if (selectedRole == null || selectedRole == "All")            
                return View(personData);            
            else
            {
                Role selected = (Role)Enum.Parse(typeof(Role), selectedRole);
                return View(personData.Where(p => p.Role == selected));
            }
        }
        #endregion
    }
}