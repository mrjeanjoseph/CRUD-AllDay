using Chapter25.ModelValidation.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Chapter25.ModelValidation.Models
{
    [NoJeanOnMondays] //Notice this at the model level
    public class AppointmentOld
    {
        [Required]
        public string ClientName { get; set; }

        [DataType(DataType.Date)]
        //[Required(ErrorMessage = "Please enter a date")]
        [FutureDate(ErrorMessage = "Please enter a date in the future.")]
        public DateTime AppointmentDate { get; set; }

        //[Range(typeof(bool), "true", "true", ErrorMessage = "You must accept T&Cs")]
        [MustBeTrue(ErrorMessage = "You must accept the T&Cs")]
        public bool TermsAndConditionsAccepted { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validation)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (string.IsNullOrEmpty(ClientName))
                errors.Add(new ValidationResult("Please enter your name"));

            if (DateTime.Now > AppointmentDate)
                errors.Add(new ValidationResult("Please enter a date in the future"));

            if (errors.Count == 0 && ClientName == "Jean" && AppointmentDate.DayOfWeek == DayOfWeek.Monday)
                errors.Add(new ValidationResult("Jean cannot book appointments on Mondays"));

            if (!TermsAndConditionsAccepted)
                errors.Add(new ValidationResult("You must accept all T&Cs"));

            return errors;
        }
    }
}