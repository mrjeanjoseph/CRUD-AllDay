using Ch25_ModelValidation.Models;
using System;
using System.Web.Mvc;

namespace Ch25_ModelValidation.Controllers {
    public class HomeController : Controller {
        // GET: Home

        public ViewResult Index() {
            return View("MakeBooking");
        }
        public ViewResult MakeBooking() {
            return View(new Appointment { Date = DateTime.Now });
        }

        [HttpPost]
        public ViewResult MakeBooking(Appointment appt) {

            if (string.IsNullOrEmpty(appt.ClientName))
                ModelState.AddModelError("ClientName", "Please enter your name");

            if (ModelState.IsValidField("Date") && DateTime.Now > appt.Date)
                ModelState.AddModelError("Date", "Please enter a date in the future");

            if (!appt.TermsAccepted)
                ModelState.AddModelError("TermsAccepted", "You must accept the terms");

            if (ModelState.IsValidField("ClientName") && 
                ModelState.IsValidField("Date") && 
                appt.ClientName == "Ariel" && 
                appt.Date.DayOfWeek == DayOfWeek.Monday) {
                ModelState.AddModelError("", "Ariel Henry is unable to setup an appointment for right now.");
            }

            if (ModelState.IsValid)
                return View("Completed", appt);
            else
                return View();
        }
    }
}