using System;
using System.ComponentModel.DataAnnotations;

namespace Chapter25.ModelValidation.Models
{
    public class Appointment
    {
        [Required]
        public string ClientName { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Please enter a date")]
        public DateTime AppointmentDate { get; set; }

        [Range(typeof(bool), "true", "true", ErrorMessage = "You must accept T&Cs")]
        public bool TermsAndConditionsAccepted { get; set; }
    }
}