using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TimesheetManagement.Application.Common.Abstractions;
using TimesheetManagement.Application.Projects.Queries.GetAllProjects;
using TimesheetManagement.Domain.Projects.Repositories;

namespace TimesheetManagement.Application.Projects.Queries.GetProjectByCode;
public sealed class GetProjectByCodeHandler : IQueryHandler<GetProjectByCodeQuery, ProjectDto>
{
    private readonly IProjectRepository _repo;

    public GetProjectByCodeHandler(IProjectRepository repo)
    {
        _repo = repo;
    }

    public async Task<ProjectDto> Handle(GetProjectByCodeQuery query, CancellationToken cancellationToken)
    {
        var project = await _repo.GetByCodeAsync(query.Code, cancellationToken);
        if (project is null) throw new KeyNotFoundException("Project not found");
        return new ProjectDto(project.Id, project.Code, project.Name, project.Industry, project.IsArchived);
    }
}
