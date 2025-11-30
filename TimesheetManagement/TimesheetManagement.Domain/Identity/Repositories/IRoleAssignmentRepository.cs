using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace TimesheetManagement.Domain.Identity.Repositories;
public interface IRoleAssignmentRepository
{
    Task<RoleAssignment?> GetAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(Guid adminId, Guid userId, CancellationToken cancellationToken = default);
    Task AddAsync(RoleAssignment assignment, CancellationToken cancellationToken = default);
    Task UpdateAsync(RoleAssignment assignment, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<RoleAssignment>> GetByAdminAsync(Guid adminId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<RoleAssignment>> GetByUserAsync(Guid userId, CancellationToken cancellationToken = default);
}
