using Chapter27.WebServices.Models;
using System.Web.Mvc;

namespace Chapter27.WebServices.Controllers
{
    public class HomeController : Controller
    {
        private ReservationRepository repository = ReservationRepository.Current;
        public ActionResult Index() => View(repository.GetAll());
        public ActionResult IndexKO() => View();

        public ActionResult Add(Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                repository.Add(reservation);
                return RedirectToAction("IndexKO");
            }
            else return View(reservation);
        }

        public ActionResult Remove(int reservationId)
        {
            repository.Remove(reservationId);
            return RedirectToAction("Index");
        }

        public ActionResult Update(Reservation reservation)
        {
            if (ModelState.IsValid && repository.Update(reservation))
                return RedirectToAction("Index");
            else
                return View("Index");
        }
    }
}