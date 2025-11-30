using System;
using TimesheetManagement.Domain.Common;

namespace TimesheetManagement.Domain.Expenses.Events;
public sealed record ExpenseSubmittedEvent(Guid ExpenseReportId, Guid UserId, DateOnly From, DateOnly To, DateTime OccurredOn) : IDomainEvent;
