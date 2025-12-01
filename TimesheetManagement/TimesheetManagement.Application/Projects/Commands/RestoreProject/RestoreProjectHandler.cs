using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TimesheetManagement.Application.Common.Abstractions;
using TimesheetManagement.Domain.Projects.Repositories;

namespace TimesheetManagement.Application.Projects.Commands.RestoreProject;
public sealed class RestoreProjectHandler : ICommandHandler<RestoreProjectCommand, bool>
{
    private readonly IProjectRepository _repo;
    private readonly IUnitOfWork _uow;

    public RestoreProjectHandler(IProjectRepository repo, IUnitOfWork uow)
    {
        _repo = repo;
        _uow = uow;
    }

    public async Task<bool> Handle(RestoreProjectCommand command, CancellationToken cancellationToken)
    {
        var project = await _repo.GetAsync(command.ProjectId, cancellationToken);
        if (project is null) throw new KeyNotFoundException("Project not found");
        project.Restore();
        await _repo.UpdateAsync(project, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);
        return true;
    }
}
