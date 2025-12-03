using System;
using TimesheetManagement.Domain.Common;
using TimesheetManagement.Domain.TimeTracking.ValueObjects;

namespace TimesheetManagement.Domain.TimeTracking;
public class TimeEntry : Entity
{
    public Guid ProjectId { get; private set; }
    public DateOnly Date { get; private set; }
    public HoursWorked Hours { get; private set; }
    public string? Notes { get; private set; }

    private TimeEntry() { }

    public TimeEntry(Guid projectId, DateOnly date, HoursWorked hours, string? notes = null)
    {
        ProjectId = projectId;
        Date = date;
        Hours = hours;
        Notes = notes;
    }

    public void Update(HoursWorked hours, string? notes)
    {
        Hours = hours;
        Notes = notes;
    }
}
