using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TimesheetManagement.Domain.Identity;
using TimesheetManagement.Domain.Identity.Repositories;
using TimesheetManagement.Infrastructure.Persistence;

namespace TimesheetManagement.Infrastructure.Repositories;

public sealed class RoleAssignmentRepository : IRoleAssignmentRepository
{
    private readonly AppDbContext _db;
    public RoleAssignmentRepository(AppDbContext db) => _db = db;

    public async Task<RoleAssignment?> GetAsync(Guid id, CancellationToken cancellationToken = default)
        => await _db.RoleAssignments.FirstOrDefaultAsync(r => r.Id == id, cancellationToken);

    public async Task<bool> ExistsAsync(Guid adminId, Guid userId, CancellationToken cancellationToken = default)
        => await _db.RoleAssignments.AnyAsync(r => r.AdminId == adminId && r.UserId == userId, cancellationToken);

    public async Task AddAsync(RoleAssignment assignment, CancellationToken cancellationToken = default)
        => await _db.RoleAssignments.AddAsync(assignment, cancellationToken);

    public Task UpdateAsync(RoleAssignment assignment, CancellationToken cancellationToken = default)
    {
        _db.RoleAssignments.Update(assignment);
        return Task.CompletedTask;
    }

    public async Task<IReadOnlyList<RoleAssignment>> GetByAdminAsync(Guid adminId, CancellationToken cancellationToken = default)
        => await _db.RoleAssignments.Where(r => r.AdminId == adminId).ToListAsync(cancellationToken);

    public async Task<IReadOnlyList<RoleAssignment>> GetByUserAsync(Guid userId, CancellationToken cancellationToken = default)
        => await _db.RoleAssignments.Where(r => r.UserId == userId).ToListAsync(cancellationToken);
}
