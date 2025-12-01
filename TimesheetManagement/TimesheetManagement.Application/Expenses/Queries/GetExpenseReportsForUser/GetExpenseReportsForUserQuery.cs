using System;
using System.Collections.Generic;
using TimesheetManagement.Application.Common.Abstractions;

namespace TimesheetManagement.Application.Expenses.Queries.GetExpenseReportsForUser;

public sealed record GetExpenseReportsForUserQuery(Guid UserId, DateOnly? From = null, DateOnly? To = null) : IQuery<IReadOnlyList<ExpenseReportSummaryDto>>;

public sealed record ExpenseReportSummaryDto(Guid Id, DateOnly From, DateOnly To, string Status, int ItemCount);
