using TimesheetManagement.Application.Common.Abstractions;
using TimesheetManagement.Domain.Projects.Repositories;

namespace TimesheetManagement.Application.Projects.Commands.ArchiveProject;
public sealed class ArchiveProjectHandler : ICommandHandler<ArchiveProjectCommand, bool>
{
    private readonly IProjectRepository _repo;
    private readonly IUnitOfWork _uow;

    public ArchiveProjectHandler(IProjectRepository repo, IUnitOfWork uow)
    {
        _repo = repo;
        _uow = uow;
    }

    public async Task<bool> Handle(ArchiveProjectCommand command, CancellationToken cancellationToken)
    {
        var project = await _repo.GetAsync(command.ProjectId, cancellationToken);
        if (project is null) throw new KeyNotFoundException("Project not found");
        project.Archive();
        await _repo.UpdateAsync(project, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);
        return true;
    }
}
