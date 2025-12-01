namespace TimesheetManagement.Application.Expenses.Queries.GetExpenseReportsForUser;

using TimesheetManagement.Application.Common.Abstractions;

public sealed record GetExpenseReportsForUserQuery(Guid UserId, DateOnly? From = null, DateOnly? To = null) : IQuery<IReadOnlyList<ExpenseReportSummaryDto>>;

public sealed record ExpenseReportSummaryDto(Guid Id, DateOnly From, DateOnly To, string Status, int ItemCount);
