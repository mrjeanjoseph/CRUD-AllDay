using Ch27_WebServices.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace Ch27_WebServices.Controllers {
    public class CustomWebController : ApiController {
        private ReservationRepository reservationRepo = ReservationRepository.Current;

        public IEnumerable<Reservation> GetAllReservations() {
            return reservationRepo.GetAllReservation();
        }

        public Reservation GetReservation(int id) {
            return reservationRepo.GetReservationById(id);
        }

        public Reservation PostReservation(Reservation reservation) {
            return reservationRepo.AddNewReservation(reservation);
        }

        public bool PutReservation(Reservation reservation) {
            return reservationRepo.UpdateReservation(reservation);
        }

        public void DeleteReservation(int id) {
            reservationRepo.RemoveReservation(id);
        }
    }
}
