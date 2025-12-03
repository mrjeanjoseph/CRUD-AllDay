using TimesheetManagement.Application.Common.Abstractions;
using TimesheetManagement.Domain.Projects.Repositories;

namespace TimesheetManagement.Application.Projects.Queries.GetAllProjects;
public sealed class GetAllProjectsHandler : IQueryHandler<GetAllProjectsQuery, IReadOnlyList<ProjectDto>>
{
    private readonly IProjectRepository _repo;

    public GetAllProjectsHandler(IProjectRepository repo)
    {
        _repo = repo;
    }

    public async Task<IReadOnlyList<ProjectDto>> Handle(GetAllProjectsQuery query, CancellationToken cancellationToken)
    {
        var projects = await _repo.GetAllAsync(cancellationToken);
        return projects.Select(p => new ProjectDto(p.Id, p.Code, p.Name, p.Industry, p.IsArchived)).ToList();
    }
}
