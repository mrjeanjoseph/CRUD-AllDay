using Microsoft.EntityFrameworkCore;
using TimesheetManagement.Domain.Expenses;
using TimesheetManagement.Domain.Expenses.Repositories;
using TimesheetManagement.Infrastructure.Persistence;

namespace TimesheetManagement.Infrastructure.Repositories;

public sealed class ExpenseReportRepository : IExpenseReportRepository
{
    private readonly AppDbContext _db;
    public ExpenseReportRepository(AppDbContext db) => _db = db;

    public async Task<ExpenseReport?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _db.ExpenseReports
            .Include(r => r.Items)
            .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<ExpenseReport>> GetForUserAsync(Guid userId, DateOnly? from = null, DateOnly? to = null, CancellationToken cancellationToken = default)
    {
        var query = _db.ExpenseReports
            .Include(r => r.Items)
            .Where(r => r.UserId == userId);
        
        // SQLite should handle these better than EF in-memory
        // Note: For production with real SQL Server, these would be optimized with proper indexes
        var list = await query.ToListAsync(cancellationToken);
        
        // Filter in memory for owned type properties to ensure compatibility
        if (from.HasValue)
            list = list.Where(r => r.Period.From >= from.Value).ToList();
        if (to.HasValue)
            list = list.Where(r => r.Period.To <= to.Value).ToList();
            
        return list;
    }

    public async Task AddAsync(ExpenseReport report, CancellationToken cancellationToken = default)
        => await _db.ExpenseReports.AddAsync(report, cancellationToken);

    public Task UpdateAsync(ExpenseReport report, CancellationToken cancellationToken = default)
    {
        _db.ExpenseReports.Update(report);
        return Task.CompletedTask;
    }

    public async Task<bool> HasSubmittedForRangeAsync(Guid userId, DateOnly from, DateOnly to, CancellationToken cancellationToken = default)
    {
        var reports = await _db.ExpenseReports
            .Where(r => r.UserId == userId && r.Status == ExpenseStatus.Submitted)
            .ToListAsync(cancellationToken);
            
        return reports.Any(r => r.Period.From == from && r.Period.To == to);
    }

    public async Task<IReadOnlyList<ExpenseReport>> GetPendingForApprovalAsync(Guid? adminUserId = null, CancellationToken cancellationToken = default)
    {
        var query = _db.ExpenseReports
            .Include(r => r.Items)
            .Where(r => r.Status == ExpenseStatus.Submitted);
            
        if (adminUserId.HasValue)
        {
            // Filter by team if needed
        }
        
        return await query.ToListAsync(cancellationToken);
    }
}
