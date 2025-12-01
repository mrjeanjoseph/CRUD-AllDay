using System;
using System.Threading;
using System.Threading.Tasks;
using TimesheetManagement.Application.Common.Abstractions;
using TimesheetManagement.Domain.Projects;
using TimesheetManagement.Domain.Projects.Repositories;

namespace TimesheetManagement.Application.Projects.Commands.CreateProject;
public sealed class CreateProjectHandler : ICommandHandler<CreateProjectCommand, Guid>
{
    private readonly IProjectRepository _repo;
    private readonly IUnitOfWork _uow;

    public CreateProjectHandler(IProjectRepository repo, IUnitOfWork uow)
    {
        _repo = repo;
        _uow = uow;
    }

    public async Task<Guid> Handle(CreateProjectCommand command, CancellationToken cancellationToken)
    {
        if (await _repo.CodeExistsAsync(command.Code, cancellationToken)) throw new InvalidOperationException("Project code already exists");
        if (await _repo.NameExistsAsync(command.Name, cancellationToken)) throw new InvalidOperationException("Project name already exists");

        var project = new Project(command.Code, command.Name, command.Industry);
        await _repo.AddAsync(project, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);
        return project.Id;
    }
}
