using Microsoft.EntityFrameworkCore;
using TimesheetManagement.Domain.TimeTracking;
using TimesheetManagement.Domain.TimeTracking.Repositories;
using TimesheetManagement.Infrastructure.Persistence;

namespace TimesheetManagement.Infrastructure.Repositories;

public sealed class TimeSheetRepository : ITimeSheetRepository
{
    private readonly AppDbContext _db;
    public TimeSheetRepository(AppDbContext db) => _db = db;

    public async Task<TimeSheet?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _db.TimeSheets
            .Include(s => s.Entries)
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<TimeSheet>> GetForUserAsync(Guid userId, DateOnly? from = null, DateOnly? to = null, CancellationToken cancellationToken = default)
    {
        var query = _db.TimeSheets
            .Include(s => s.Entries)
            .Where(s => s.UserId == userId);
            
        // SQLite should handle these better than EF in-memory
        // Note: For production with real SQL Server, these would be optimized with proper indexes
        var list = await query.ToListAsync(cancellationToken);
        
        // Filter in memory for owned type properties to ensure compatibility
        if (from.HasValue)
            list = list.Where(s => s.Period.From >= from.Value).ToList();
        if (to.HasValue)
            list = list.Where(s => s.Period.To <= to.Value).ToList();
            
        return list;
    }

    public async Task AddAsync(TimeSheet sheet, CancellationToken cancellationToken = default)
        => await _db.TimeSheets.AddAsync(sheet, cancellationToken);

    public Task UpdateAsync(TimeSheet sheet, CancellationToken cancellationToken = default)
    {
        _db.TimeSheets.Update(sheet);
        return Task.CompletedTask;
    }

    public async Task<bool> HasSubmittedForRangeAsync(Guid userId, DateOnly from, DateOnly to, CancellationToken cancellationToken = default)
    {
        var sheets = await _db.TimeSheets
            .Where(s => s.UserId == userId && s.Status == TimeSheetStatus.Submitted)
            .ToListAsync(cancellationToken);
            
        return sheets.Any(s => s.Period.From == from && s.Period.To == to);
    }

    public async Task<IReadOnlyList<TimeSheet>> GetPendingForApprovalAsync(Guid? adminUserId = null, CancellationToken cancellationToken = default)
    {
        var query = _db.TimeSheets
            .Include(s => s.Entries)
            .Where(s => s.Status == TimeSheetStatus.Submitted);
            
        if (adminUserId.HasValue)
        {
            // Assuming admin can approve based on team membership; for simplicity, return all for now
            // In real app, filter by team
        }
        
        return await query.ToListAsync(cancellationToken);
    }
}
