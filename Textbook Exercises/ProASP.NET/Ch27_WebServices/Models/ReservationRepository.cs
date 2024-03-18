using System.Collections.Generic;
using System.Linq;

namespace Ch27_WebServices.Models {
    public class ReservationRepository {

        private readonly static ReservationRepository _instance = new ReservationRepository();

        public static ReservationRepository Current { get { return _instance; } }

        private List<Reservation> _reservationData = new List<Reservation> {
            new Reservation { ReservationId = 1, ClientName = "Jovenel Moise", ReservedLocation = "Cap-Haitian" },
            new Reservation { ReservationId = 2, ClientName = "Ariel Henry", ReservedLocation = "Jeremy" },
            new Reservation { ReservationId = 3, ClientName = "Henry Christophe", ReservedLocation = "Plateau-Central" },
            new Reservation { ReservationId = 4, ClientName = "Michelle Martelli", ReservedLocation = "Port-de-Paix" },
            new Reservation { ReservationId = 5, ClientName = "Rene Garcia Preval", ReservedLocation = "Port-au-Prince" },
        };

        public IEnumerable<Reservation> GetAllReservation() { return _reservationData; }

        public Reservation GetReservationById(int id) {
            return _reservationData.Where(r => r.ReservationId == id).FirstOrDefault();
        }

        public Reservation AddNewReservation(Reservation reservation) {
            reservation.ReservationId = _reservationData.Count + 1;
            _reservationData.Add(reservation);
            return reservation;
        }

        public void RemoveReservation(int id) {
            Reservation reservation = GetReservationById(id);
            if (reservation != null) {
                _reservationData.Remove(reservation);
            }
        }

        public bool UpdateReservation(Reservation reservation) {
            Reservation storedReservation = GetReservationById(reservation.ReservationId);
            if (storedReservation != null) {
                storedReservation.ClientName = reservation.ClientName;
                storedReservation.ReservedLocation = reservation.ReservedLocation;
                return true;
            } else
                return false;            
        }
    }
}