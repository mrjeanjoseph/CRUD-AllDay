namespace TimesheetManagement.Domain.Common.ValueObjects;
public record DateRange
{
    public DateOnly From { get; }
    public DateOnly To { get; }

    public DateRange(DateOnly from, DateOnly to)
    {
        if (to < from) throw new ArgumentException("To must be >= From", nameof(to));
        From = from;
        To = to;
    }

    public int TotalDays => To.DayNumber - From.DayNumber + 1;
    public bool Contains(DateOnly date) => date >= From && date <= To;
}
