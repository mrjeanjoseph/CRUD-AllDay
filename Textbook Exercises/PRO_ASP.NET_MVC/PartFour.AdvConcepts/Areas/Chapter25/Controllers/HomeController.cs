using Chapter25.ModelValidation.Models;
using System;
using System.Web.Mvc;

namespace Chapter25.ModelValidation.Controllers
{
    public class HomeController : Controller
    {
        // GET: Chapter25/Home
        public ActionResult Index()
        {
            return View();
        }

        public ViewResult MakeBooking()
        {
            return View(new Appointment { AppointmentDate = DateTime.Now });
        }

        [HttpPost]
        public ViewResult MakeBooking(Appointment appointment)
        {
            if (ModelState.IsValid)
                return View("BookingCompleted", appointment);
            else return View();
        }

        [HttpPost]
        public ViewResult MakeBookingTwo(Appointment appointment)
        {
            if (string.IsNullOrEmpty(appointment.ClientName))
                ModelState.AddModelError("ClientName", "Please Enter your name");

            if (ModelState.IsValidField("AppointmentDate") && DateTime.Now > appointment.AppointmentDate)
                ModelState.AddModelError("AppointmentDate", "Please enter a date in the future");

            if (!appointment.TermsAndConditionsAccepted)
                ModelState.AddModelError("TermsAndConditionsAccepted", "You must accept the T&Cs");

            if (ModelState.IsValidField("ClientName") && ModelState.IsValidField("AppointmentDate")
                && appointment.ClientName == "Jean" && appointment.AppointmentDate.DayOfWeek == DayOfWeek.Monday)
                ModelState.AddModelError("", "Jean cannot book appointments on Mondays");

            if (ModelState.IsValid)
                return View("BookingCompleted", appointment);
            else return View();

        }
    }
}