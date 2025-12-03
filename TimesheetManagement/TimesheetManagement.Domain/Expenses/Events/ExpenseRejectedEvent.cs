using TimesheetManagement.Domain.Common;

namespace TimesheetManagement.Domain.Expenses.Events;
public sealed record ExpenseRejectedEvent(Guid ExpenseReportId, Guid UserId, string Comment, DateTime OccurredOn) : IDomainEvent;
