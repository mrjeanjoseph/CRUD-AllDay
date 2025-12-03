using System;
using TimesheetManagement.Application.Common.Abstractions;

namespace TimesheetManagement.Application.Expenses.Commands.RejectExpenseReport;
public sealed record RejectExpenseReportCommand(Guid ExpenseReportId, Guid AdminId, string Comment) : ICommand<bool>;
