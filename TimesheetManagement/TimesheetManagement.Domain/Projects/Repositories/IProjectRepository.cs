using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace TimesheetManagement.Domain.Projects.Repositories;
public interface IProjectRepository
{
    Task<Project?> GetAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Project?> GetByCodeAsync(string code, CancellationToken cancellationToken = default);
    Task<bool> CodeExistsAsync(string code, CancellationToken cancellationToken = default);
    Task<bool> NameExistsAsync(string name, CancellationToken cancellationToken = default);
    Task AddAsync(Project project, CancellationToken cancellationToken = default);
    Task UpdateAsync(Project project, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Project>> GetAllAsync(CancellationToken cancellationToken = default);
}
