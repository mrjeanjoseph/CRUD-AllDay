using TimesheetManagement.Domain.Common;

namespace TimesheetManagement.Domain.Expenses.Events;
public sealed record ExpenseApprovedEvent(Guid ExpenseReportId, Guid UserId, string Comment, DateTime OccurredOn) : IDomainEvent;
