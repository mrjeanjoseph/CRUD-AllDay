using Chapter27.WebServices.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace Chapter27.WebServices.Controllers
{
    public class WebController : ApiController
    {
        private readonly ReservationRepository repository = ReservationRepository.Current;

        public IEnumerable<Reservation> GetAllReservations() => repository.GetAll();

        public Reservation GetReservation(int reservationId ) => repository.Get(reservationId);

        [HttpPost]
        public Reservation CreateReservation(Reservation reservation) => repository.Add(reservation);

        [HttpPut]
        public bool UpdateReservation(Reservation reservation) => repository.Update(reservation);

        [HttpDelete] 
        public void DeleteReservation(int reservationId) => repository.Remove(reservationId);
        
    }
}
