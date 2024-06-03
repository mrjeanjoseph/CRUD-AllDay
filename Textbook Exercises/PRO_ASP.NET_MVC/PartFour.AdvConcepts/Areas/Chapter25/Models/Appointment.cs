using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Chapter25.ModelValidation.Models
{
    public class Appointment
    {
        [Required]
        [StringLength(10, MinimumLength = 3)]
        public string ClientName { get; set; }

        [DataType(DataType.Date)]
        [Remote("ValidateDate", "Home")]
        public DateTime AppointmentDate { get; set; }

        public bool TermsAndConditionsAccepted { get; set; }

    }
}