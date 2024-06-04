using Chapter26.Bundles.Models;
using System;
using System.Web.Mvc;

namespace Chapter26.Bundles.Controllers
{
    public class HomeController : Controller
    {
        // GET: Chapter26/Home
        public ActionResult Index()
        {
            return View();
        }

        public ViewResult MakeBooking()
        {
            return View(new Appointment
            {
                ClientName = "Julie Duree",
                AppointmentDate = DateTime.Now,
                TermsAndConditionsAccepted = true
            });
        }

        [HttpPost]
        public JsonResult MakeBooking(Appointment appointment)
        {
            return Json(appointment, JsonRequestBehavior.AllowGet);
        }
    }
}