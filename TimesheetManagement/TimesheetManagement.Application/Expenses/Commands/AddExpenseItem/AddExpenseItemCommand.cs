using TimesheetManagement.Application.Common.Abstractions;

namespace TimesheetManagement.Application.Expenses.Commands.AddExpenseItem;
public sealed record AddExpenseItemCommand(
    Guid ExpenseReportId,
    DateOnly Date,
    string Category,
    decimal Amount,
    string Currency,
    string? ReceiptPath,
    string? Notes) : ICommand<bool>;
