using System;
using TimesheetManagement.Application.Common.Abstractions;

namespace TimesheetManagement.Application.Expenses.Commands.SubmitExpenseReport;
public sealed record SubmitExpenseReportCommand(Guid ExpenseReportId) : ICommand<bool>;
