using System.ComponentModel.DataAnnotations;

namespace Ch26_ClientFeatures.Models {
    public class Appointment {

        [Required]
        public string ClientName { get; set; }

        public bool TermsAccepted { get; set; }
    }
}