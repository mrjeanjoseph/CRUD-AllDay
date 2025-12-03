using TimesheetManagement.Domain.Common;
using TimesheetManagement.Domain.Common.ValueObjects;
using TimesheetManagement.Domain.TimeTracking.Events;

namespace TimesheetManagement.Domain.TimeTracking;

public enum TimeSheetStatus
{
    Draft = 0,
    Submitted = 1,
    Approved = 2,
    Rejected = 3
}

public class TimeSheet : Entity
{
    public Guid UserId { get; private set; }
    public DateRange Period { get; private set; }
    public TimeSheetStatus Status { get; private set; } = TimeSheetStatus.Draft;
    public string? Comment { get; private set; }

    private readonly List<TimeEntry> _entries = new();
    public IReadOnlyList<TimeEntry> Entries => _entries;

    private readonly List<object> _domainEvents = new();
    public IReadOnlyList<object> DomainEvents => _domainEvents;

    private TimeSheet() { }

    public TimeSheet(Guid userId, DateOnly from, DateOnly to)
    {
        UserId = userId;
        Period = new DateRange(from, to);
    }

    public void AddEntry(TimeEntry entry)
    {
        if (Status != TimeSheetStatus.Draft) throw new InvalidOperationException("Cannot modify non-draft timesheet");
        if (!Period.Contains(entry.Date)) throw new ArgumentOutOfRangeException(nameof(entry.Date));
        _entries.Add(entry);
    }

    public void RemoveEntry(Guid entryId)
    {
        if (Status != TimeSheetStatus.Draft) throw new InvalidOperationException("Cannot modify non-draft timesheet");
        _entries.RemoveAll(e => e.Id == entryId);
    }

    public void Submit()
    {
        if (_entries.Count == 0) throw new InvalidOperationException("Timesheet must have at least one entry");
        Status = TimeSheetStatus.Submitted;
        Comment = null;
        _domainEvents.Add(new TimeSheetSubmittedEvent(Id, UserId, Period.From, Period.To, DateTime.UtcNow));
    }

    public void Approve(string? comment = null)
    {
        if (Status != TimeSheetStatus.Submitted) throw new InvalidOperationException("Only submitted timesheets can be approved");
        Status = TimeSheetStatus.Approved;
        Comment = comment;
        _domainEvents.Add(new TimeSheetApprovedEvent(Id, UserId, Comment, DateTime.UtcNow));
    }

    public void Reject(string comment)
    {
        if (Status != TimeSheetStatus.Submitted) throw new InvalidOperationException("Only submitted timesheets can be rejected");
        if (string.IsNullOrWhiteSpace(comment)) throw new ArgumentException("Rejection requires a comment", nameof(comment));
        Status = TimeSheetStatus.Rejected;
        Comment = comment;
        _domainEvents.Add(new TimeSheetRejectedEvent(Id, UserId, comment, DateTime.UtcNow));
    }

    public void EditAfterRejection()
    {
        if (Status != TimeSheetStatus.Rejected) throw new InvalidOperationException("Only rejected timesheets can be edited");
        Status = TimeSheetStatus.Draft;
    }

    public void ClearDomainEvents() => _domainEvents.Clear();
}
