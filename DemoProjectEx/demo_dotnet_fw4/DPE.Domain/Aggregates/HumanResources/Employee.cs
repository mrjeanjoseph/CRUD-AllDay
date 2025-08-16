using System;

namespace DPE.Domain.Aggregates.HumanResources
{
    public class Employee
    {
        public int Id { get; private set; }
        public NationalId NationalId { get; private set; }
        public string LoginId { get; private set; }
        public string JobTitle { get; private set; }
        public DateTime HireDate { get; private set; }
        public bool IsSalaried { get; private set; }
        public int VacationHours { get; private set; }
        public int SickLeaveHours { get; private set; }
        public bool IsActive { get; private set; }

        public Employee(
            int id,
            NationalId nationalId,
            string loginId,
            string jobTitle,
            DateTime hireDate,
            bool isSalaried,
            int vacationHours,
            int sickLeaveHours,
            bool isActive)
        {
            Id = id;
            NationalId = nationalId;
            LoginId = loginId;
            JobTitle = jobTitle;
            HireDate = hireDate;
            IsSalaried = isSalaried;
            VacationHours = vacationHours;
            SickLeaveHours = sickLeaveHours;
            IsActive = isActive;
        }

        public bool IsEligibleForLeave() => VacationHours > 0 || SickLeaveHours > 0;
    }
}