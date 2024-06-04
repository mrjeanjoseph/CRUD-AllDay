using System;
using System.ComponentModel.DataAnnotations;

namespace Chapter26.Bundles.Models
{
    public class Appointment
    {
        [Required]
        public string ClientName { get; set; }

        [DataType(DataType.Date)]
        public DateTime AppointmentDate { get; set; }

        public bool TermsAndConditionsAccepted { get; set; }

    }
}