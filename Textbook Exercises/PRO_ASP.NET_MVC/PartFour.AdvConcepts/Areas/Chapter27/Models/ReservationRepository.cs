using System.Collections.Generic;
using System.Linq;

namespace Chapter27.WebServices.Models
{
    public class ReservationRepository
    {
        private static readonly ReservationRepository repository = new ReservationRepository();

        public static ReservationRepository Current
        {
            get { return repository; }
        }

        private List<Reservation> data = new List<Reservation> {
            new Reservation { 
                ReservationId = 5, 
                ClientName = "Kervens Jean-Joseph", 
                ReservationName = "This is the reservation details description",
                Location = "St. Pete"
            },
            new Reservation { 
                ReservationId = 10, 
                ClientName = "Denzel Jean-Joseph", 
                ReservationName = "This is the reservation details description", 
                Location = "Lakeland"
            },
            new Reservation { 
                ReservationId = 15, 
                ClientName = "Elanie Jean-Joseph", 
                ReservationName = "This is the reservation details description", 
                Location = "SixHubs, Tx"
            },
            new Reservation { 
                ReservationId = 20,
                ClientName = "Denzel Paniague",
                ReservationName = "This is the reservation details description",
                Location = "Santo Domingo"
            },
            new Reservation { 
                ReservationId = 25, 
                ClientName = "Ethan Jean-Joseph", 
                ReservationName = "This is the reservation details description", 
                Location = "Venice"
            },
        };

        public IEnumerable<Reservation> GetAll() => data;

        public Reservation Get(int reservationId) =>
            data.Where(r => r.ReservationId == reservationId).FirstOrDefault();

        public Reservation Add(Reservation reservation)
        {
            reservation.ReservationId = data.Count + 1;
            data.Add(reservation);
            return reservation;
        }

        public void Remove(int reservationId)
        {
            Reservation reservationItem = Get(reservationId);
            if (reservationItem != null) data.Remove(reservationItem);
        }

        public bool Update(Reservation reservation)
        {
            Reservation storedReservation = Get(reservation.ReservationId);
            if (storedReservation != null)
            {
                storedReservation.ClientName = reservation.ClientName;
                storedReservation.Location = reservation.Location;
                return true;
            }
            else
                return false;
        }
    }
}