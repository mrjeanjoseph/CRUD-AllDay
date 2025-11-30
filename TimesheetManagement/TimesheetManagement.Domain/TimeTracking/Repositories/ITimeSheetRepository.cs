using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace TimesheetManagement.Domain.TimeTracking.Repositories;
public interface ITimeSheetRepository
{
    Task<TimeSheet?> GetAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<TimeSheet>> GetForUserAsync(Guid userId, DateOnly? from = null, DateOnly? to = null, CancellationToken cancellationToken = default);
    Task AddAsync(TimeSheet sheet, CancellationToken cancellationToken = default);
    Task UpdateAsync(TimeSheet sheet, CancellationToken cancellationToken = default);
    Task<bool> HasSubmittedForRangeAsync(Guid userId, DateOnly from, DateOnly to, CancellationToken cancellationToken = default);
}
