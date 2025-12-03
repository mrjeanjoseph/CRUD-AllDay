using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TimesheetManagement.Domain.Projects;
using TimesheetManagement.Domain.Projects.Repositories;
using TimesheetManagement.Infrastructure.Persistence;

namespace TimesheetManagement.Infrastructure.Repositories;

public sealed class ProjectRepository : IProjectRepository
{
    private readonly AppDbContext _db;
    public ProjectRepository(AppDbContext db) => _db = db;

    public async Task<Project?> GetAsync(Guid id, CancellationToken cancellationToken = default)
        => await _db.Projects.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

    public async Task<Project?> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
        => await _db.Projects.FirstOrDefaultAsync(p => p.Code == code, cancellationToken);

    public async Task<bool> CodeExistsAsync(string code, CancellationToken cancellationToken = default)
        => await _db.Projects.AnyAsync(p => p.Code == code, cancellationToken);

    public async Task<bool> NameExistsAsync(string name, CancellationToken cancellationToken = default)
        => await _db.Projects.AnyAsync(p => p.Name == name, cancellationToken);

    public async Task AddAsync(Project project, CancellationToken cancellationToken = default)
        => await _db.Projects.AddAsync(project, cancellationToken);

    public Task UpdateAsync(Project project, CancellationToken cancellationToken = default)
    {
        _db.Projects.Update(project);
        return Task.CompletedTask;
    }

    public async Task<IReadOnlyList<Project>> GetAllAsync(CancellationToken cancellationToken = default)
        => await _db.Projects.ToListAsync(cancellationToken);
}
