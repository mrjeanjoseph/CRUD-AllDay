using Ch27_WebServices.Models;
using System.Web.Mvc;

namespace Ch27_WebServices.Controllers {
    public class HomeController : Controller {

        private readonly ReservationRepository _repo = ReservationRepository.Current;

        // GET: Home
        public ActionResult Index() {
            return View(_repo.GetAllReservation());
        }

        public ActionResult AddNewReservation(Reservation reservation) {
            if (ModelState.IsValid) {
                _repo.AddNewReservation(reservation);
                return RedirectToAction("Index");

            } else return View("Index");
        }

        public ActionResult UpdateReservation(Reservation reservation) {
            if (ModelState.IsValid && _repo.UpdateReservation(reservation)) {
                return RedirectToAction("Index");

            } else return View("Index");
        }

        public ActionResult DeleteReservation(int id) {
            _repo.RemoveReservation(id);
            return RedirectToAction("Index");
        }
    }
}