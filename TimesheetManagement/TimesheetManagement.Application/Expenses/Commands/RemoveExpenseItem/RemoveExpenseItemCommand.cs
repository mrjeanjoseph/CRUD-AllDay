using TimesheetManagement.Application.Common.Abstractions;

namespace TimesheetManagement.Application.Expenses.Commands.RemoveExpenseItem;
public sealed record RemoveExpenseItemCommand(Guid ExpenseReportId, Guid ItemId) : ICommand<bool>;
