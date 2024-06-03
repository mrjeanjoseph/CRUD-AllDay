using Chapter25.ModelValidation.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Chapter25.ModelValidation.Infrastructure
{
    public class NoJeanOnMondaysAttribute : ValidationAttribute
    {
        public NoJeanOnMondaysAttribute()
        {
            ErrorMessage = "Jean cannot book appointment on Mondays";
        }

        public override bool IsValid(object value)
        {
            if (!(value is Appointment appointment) || string.IsNullOrEmpty(appointment.ClientName) ||
                appointment.AppointmentDate == null)
                return true;
            else
                return !(appointment.ClientName == "Jean" && appointment.AppointmentDate.DayOfWeek == DayOfWeek.Monday);
        }

        //public override bool IsValid(object value)
        //{
        //    Appointment appointment = value as Appointment;
        //    if (appointment == null || string.IsNullOrEmpty(appointment.ClientName) ||
        //        appointment.AppointmentDate == null)
        //        return true;
        //    else
        //        return !(appointment.ClientName == "Jean" && appointment.AppointmentDate.DayOfWeek == DayOfWeek.Monday);
        //}
    }

}