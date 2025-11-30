using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace TimesheetManagement.Domain.Expenses.Repositories;
public interface IExpenseReportRepository
{
    Task<ExpenseReport?> GetAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ExpenseReport>> GetForUserAsync(Guid userId, DateOnly? from = null, DateOnly? to = null, CancellationToken cancellationToken = default);
    Task AddAsync(ExpenseReport report, CancellationToken cancellationToken = default);
    Task UpdateAsync(ExpenseReport report, CancellationToken cancellationToken = default);
    Task<bool> HasSubmittedForRangeAsync(Guid userId, DateOnly from, DateOnly to, CancellationToken cancellationToken = default);
}
