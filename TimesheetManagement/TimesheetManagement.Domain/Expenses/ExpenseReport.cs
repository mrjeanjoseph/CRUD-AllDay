using TimesheetManagement.Domain.Common;
using TimesheetManagement.Domain.Common.ValueObjects;
using TimesheetManagement.Domain.Expenses.Events;

namespace TimesheetManagement.Domain.Expenses;
public enum ExpenseStatus
{
    Draft = 0,
    Submitted = 1,
    Approved = 2,
    Rejected = 3
}

public class ExpenseReport : Entity
{
    public Guid UserId { get; private set; }
    public DateRange Period { get; private set; }
    public ExpenseStatus Status { get; private set; } = ExpenseStatus.Draft;
    public string? Comment { get; private set; }

    private readonly List<ExpenseItem> _items = new();
    public IReadOnlyList<ExpenseItem> Items => _items;

    private readonly List<IDomainEvent> _domainEvents = new();
    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents;

    private ExpenseReport() { }

    public ExpenseReport(Guid userId, DateOnly from, DateOnly to)
    {
        UserId = userId;
        Period = new DateRange(from, to);
    }

    public void AddItem(ExpenseItem item)
    {
        if (Status != ExpenseStatus.Draft) throw new InvalidOperationException("Cannot modify non-draft expense report");
        if (!Period.Contains(item.Date)) throw new ArgumentOutOfRangeException(nameof(item.Date));
        _items.Add(item);
    }

    public void RemoveItem(Guid itemId) => _items.RemoveAll(i => i.Id == itemId);

    public void Submit()
    {
        if (_items.Count == 0) throw new InvalidOperationException("Expense report must have at least one item");
        Status = ExpenseStatus.Submitted;
        Comment = null;
        _domainEvents.Add(new ExpenseSubmittedEvent(Id, UserId, Period.From, Period.To, DateTime.UtcNow));
    }

    public void Approve(string? comment = null)
    {
        if (Status != ExpenseStatus.Submitted) throw new InvalidOperationException("Only submitted expense reports can be approved");
        Status = ExpenseStatus.Approved;
        Comment = comment;
        _domainEvents.Add(new ExpenseApprovedEvent(Id, UserId, Comment, DateTime.UtcNow));
    }

    public void Reject(string comment)
    {
        if (Status != ExpenseStatus.Submitted) throw new InvalidOperationException("Only submitted expense reports can be rejected");
        if (string.IsNullOrWhiteSpace(comment)) throw new ArgumentException("Rejection requires a comment", nameof(comment));
        Status = ExpenseStatus.Rejected;
        Comment = comment;
        _domainEvents.Add(new ExpenseRejectedEvent(Id, UserId, comment, DateTime.UtcNow));
    }

    public void EditAfterRejection()
    {
        if (Status != ExpenseStatus.Rejected) throw new InvalidOperationException("Only rejected reports can be edited");
        Status = ExpenseStatus.Draft;
    }

    public void ClearDomainEvents() => _domainEvents.Clear();
}
