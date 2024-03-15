using Ch26_ClientFeatures.Models;
using System.Web.Mvc;

namespace Ch26_ClientFeatures.Controllers {
    public class HomeController : Controller {
        // GET: Home
        public ActionResult MakeBooking() {
            return View(new Appointment {
                ClientName = "Jovenel",
                TermsAccepted = true
            });
        }

        [HttpPost]
        public JsonResult MakeBooking(Appointment appointment) {
            return Json(appointment, JsonRequestBehavior.AllowGet);
        }
    }
}