using System;
using System.ComponentModel.DataAnnotations;

namespace Ch25_ModelValidation.Models {
    public class Appointment {
        public string ClientName { get; set; }

        [DataType(DataType.Date)]
        public DateTime? Date { get; set; }

        public bool TermsAccepted { get; set; }
    }
}