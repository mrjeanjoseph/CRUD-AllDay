using System;
using TimesheetManagement.Application.Common.Abstractions;

namespace TimesheetManagement.Application.Expenses.Commands.CreateExpenseReport;
public sealed record CreateExpenseReportCommand(Guid UserId, DateOnly From, DateOnly To) : ICommand<Guid>;
