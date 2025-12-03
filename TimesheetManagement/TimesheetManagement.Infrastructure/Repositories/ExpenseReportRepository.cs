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
        var report = await _db.ExpenseReports.FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
        if (report is null) return null;
        await _db.Entry(report).Collection(r => r.Items).LoadAsync(cancellationToken);
        return report;
    }

    public async Task<IReadOnlyList<ExpenseReport>> GetForUserAsync(Guid userId, DateOnly? from = null, DateOnly? to = null, CancellationToken cancellationToken = default)
    {
        var q = _db.ExpenseReports.AsQueryable().Where(r => r.UserId == userId);
        if (from.HasValue) q = q.Where(r => r.Period.From >= from.Value);
        if (to.HasValue) q = q.Where(r => r.Period.To <= to.Value);
        var list = await q.ToListAsync(cancellationToken);
        foreach (var r in list)
        {
            await _db.Entry(r).Collection(x => x.Items).LoadAsync(cancellationToken);
        }
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
        => await _db.ExpenseReports.AnyAsync(r => r.UserId == userId && r.Status == ExpenseStatus.Submitted && r.Period.From == from && r.Period.To == to, cancellationToken);

    public async Task<IReadOnlyList<ExpenseReport>> GetPendingForApprovalAsync(Guid? adminUserId = null, CancellationToken cancellationToken = default)
    {
        var query = _db.ExpenseReports.Where(r => r.Status == ExpenseStatus.Submitted);
        if (adminUserId.HasValue)
        {
            // Filter by team if needed
        }
        return await query.Include(r => r.Items).ToListAsync(cancellationToken);
    }
}
