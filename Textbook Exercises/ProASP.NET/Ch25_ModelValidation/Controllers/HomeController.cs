using Ch25_ModelValidation.Models;
using System;
using System.Web.Mvc;

namespace Ch25_ModelValidation.Controllers {
    public class HomeController : Controller {
        // GET: Home

        public ViewResult MakeBooking() {
            return View(new Appointment { Date = DateTime.Now });
        }

        [HttpPost]
        public ViewResult MakeBooking(Appointment appt) {
            // statement to store new appointment in a
            // repository would go here in a real project

            return View("Completed", appt);
        }
    }
}