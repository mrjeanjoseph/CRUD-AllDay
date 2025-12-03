using TimesheetManagement.Application.Common.Abstractions;

namespace TimesheetManagement.Application.Expenses.Queries.GetExpenseReportById;

public sealed record GetExpenseReportByIdQuery(Guid ExpenseReportId) : IQuery<ExpenseReportDetailsDto>;

public sealed record ExpenseReportDetailsDto(
    Guid Id,
    Guid UserId,
    DateOnly From,
    DateOnly To,
    string Status,
    string? Comment,
    IReadOnlyList<ExpenseItemDto> Items);

public sealed record ExpenseItemDto(Guid Id, DateOnly Date, string Category, decimal Amount, string Currency, string? ReceiptPath, string? Notes);
