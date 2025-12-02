using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
        var sheet = await _db.TimeSheets.FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        if (sheet is null) return null;
        await _db.Entry(sheet).Collection(s => s.Entries).LoadAsync(cancellationToken);
        return sheet;
    }

    public async Task<IReadOnlyList<TimeSheet>> GetForUserAsync(Guid userId, DateOnly? from = null, DateOnly? to = null, CancellationToken cancellationToken = default)
    {
        var q = _db.TimeSheets.AsQueryable().Where(s => s.UserId == userId);
        if (from.HasValue) q = q.Where(s => s.Period.From >= from.Value);
        if (to.HasValue) q = q.Where(s => s.Period.To <= to.Value);
        var list = await q.ToListAsync(cancellationToken);
        foreach (var s in list)
        {
            await _db.Entry(s).Collection(x => x.Entries).LoadAsync(cancellationToken);
        }
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
        => await _db.TimeSheets.AnyAsync(s => s.UserId == userId && s.Status == TimeSheetStatus.Submitted && s.Period.From == from && s.Period.To == to, cancellationToken);
}
