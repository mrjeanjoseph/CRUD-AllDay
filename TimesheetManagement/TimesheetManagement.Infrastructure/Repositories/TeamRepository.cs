using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TimesheetManagement.Domain.Teams;
using TimesheetManagement.Domain.Teams.Repositories;
using TimesheetManagement.Infrastructure.Persistence;

namespace TimesheetManagement.Infrastructure.Repositories;

public sealed class TeamRepository : ITeamRepository
{
    private readonly AppDbContext _db;
    public TeamRepository(AppDbContext db) => _db = db;

    public async Task<Team?> GetAsync(Guid id, CancellationToken cancellationToken = default)
        => await _db.Teams.Include(t => t.Members).FirstOrDefaultAsync(t => t.Id == id, cancellationToken);

    public async Task<bool> NameExistsAsync(string name, CancellationToken cancellationToken = default)
        => await _db.Teams.AnyAsync(t => t.Name == name, cancellationToken);

    public async Task AddAsync(Team team, CancellationToken cancellationToken = default)
        => await _db.Teams.AddAsync(team, cancellationToken);

    public Task UpdateAsync(Team team, CancellationToken cancellationToken = default)
    {
        _db.Teams.Update(team);
        return Task.CompletedTask;
    }

    public async Task<IReadOnlyList<Team>> GetAllAsync(CancellationToken cancellationToken = default)
        => await _db.Teams.Include(t => t.Members).ToListAsync(cancellationToken);
}
