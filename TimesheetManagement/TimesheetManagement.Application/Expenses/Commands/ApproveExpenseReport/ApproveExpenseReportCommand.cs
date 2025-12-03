using System;
using TimesheetManagement.Application.Common.Abstractions;

namespace TimesheetManagement.Application.Expenses.Commands.ApproveExpenseReport;
public sealed record ApproveExpenseReportCommand(Guid ExpenseReportId, Guid AdminId, string? Comment) : ICommand<bool>;
