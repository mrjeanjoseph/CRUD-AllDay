using TimesheetManagement.Application.Common.Abstractions;

namespace TimesheetManagement.Application.Expenses.Commands.EditExpenseAfterRejection;
public sealed record EditExpenseAfterRejectionCommand(Guid ExpenseReportId) : ICommand<bool>;
