using System;
using Ch25_ModelValidation.Models;
using System.ComponentModel.DataAnnotations;

namespace Ch25_ModelValidation.Infrastructure {
    public class FilterOutPublicServant : ValidationAttribute {
        public FilterOutPublicServant() {
            ErrorMessage = "Moise, you're not able to setup an appointment right now.";
        }

        public override bool IsValid(object value) {
            Appointment app = value as Appointment;
            if (app == null || string.IsNullOrEmpty(app.ClientName) || app.Date == null)
                // I don't have a model of right type to validate or 
                // I don't have the values for the Client Name and
                // Date that I need.
                return true;
            else
                return !(app.ClientName == "Jov" && app.Date.DayOfWeek == DayOfWeek.Monday);

            //return base.IsValid(value); 
        }
    }
}