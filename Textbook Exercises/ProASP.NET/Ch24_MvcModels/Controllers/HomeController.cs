﻿using Ch24_MvcModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Ch24_MvcModels.Controllers {
    public class HomeController : Controller {

        private readonly Person[] personData = {
            new Person {PersonId = 1, FirstName = "Louna", LastName = "Jean-Joseph", Role = Role.Admin },
            new Person {PersonId = 2, FirstName = "Raoul H.", LastName = "Jean-Joseph", Role = Role.Admin },
            new Person {PersonId = 3, FirstName = "Jovenel", LastName = "Moise", Role = Role.User },
            new Person {PersonId = 4, FirstName = "Eurie", LastName = "Latortue", Role = Role.User },
            new Person {PersonId = 5, FirstName = "Denver", LastName = "Mosque", Role = Role.Guest }
        };

        public ActionResult Index(int? id = 1) {
            Person personItem = personData.Where(p => p.PersonId == id).First();
            return View(personItem);
        }

        public ActionResult CreatePerson() {
            return View(new Person());
        }

        [HttpPost]
        public ActionResult CreatePerson(Person personModel) {
            return View("Index", personModel);
        }

        public ActionResult DisplaySummary(
            [Bind(Prefix = "HomeAddress", Exclude = "Country")] AddressSummary addressSummary) {
            return View(addressSummary);
        }

        public ActionResult NameCollection(IList<string> names) {
            names = names ?? new List<string>();
            return View(names);
        }

        public ActionResult NameArray(string[] names) {
            names = names ?? new string[0];
            return View(names);
        }

        public ActionResult Address() {
            IList<AddressSummary> addresses = new List<AddressSummary>();
            UpdateModel(addresses);
            return View(addresses);
        }

        public ActionResult Address_MI1(IList<AddressSummary> addresses) {
            addresses = addresses ?? new List<AddressSummary>();
            return View(addresses);
        }

        public ActionResult Address_MI2() {
            IList<AddressSummary> addresses = new List<AddressSummary>();
            UpdateModel(addresses, new FormValueProvider(ControllerContext));
            return View(addresses);
        }

        public ActionResult Address_MI3(FormCollection formData) {
            IList<AddressSummary> addresses = new List<AddressSummary>();

            if (TryUpdateModel(addresses, formData)) {
                // Complete operation here
            } else {
                // Do something else
            }

            //We can use a try catch ere
            try {
                UpdateModel(addresses, formData);
            } catch (InvalidOperationException) {
                throw;
            }
            return View(addresses);
        }
    }
}