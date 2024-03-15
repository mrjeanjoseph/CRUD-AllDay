using System;
using System.ComponentModel.DataAnnotations;

namespace Ch25_ModelValidation.Models {

    public class Appointment {

        [Required, StringLength(10,MinimumLength =3)]
        public string ClientName { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public bool TermsAccepted { get; set; }
    }
}