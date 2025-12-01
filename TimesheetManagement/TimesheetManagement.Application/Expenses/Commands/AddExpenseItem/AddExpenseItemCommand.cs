using System;
using TimesheetManagement.Application.Common.Abstractions;
using TimesheetManagement.Domain.Expenses.ValueObjects;

namespace TimesheetManagement.Application.Expenses.Commands.AddExpenseItem;
public sealed record AddExpenseItemCommand(
    Guid ExpenseReportId,
    DateOnly Date,
    string Category,
    decimal Amount,
    string Currency,
    string? ReceiptPath,
    string? Notes) : ICommand<bool>;
