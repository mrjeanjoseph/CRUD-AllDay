using TimesheetManagement.Application.Common.Abstractions;

namespace TimesheetManagement.Application.Projects.Queries.GetAllProjects;
public sealed record GetAllProjectsQuery() : IQuery<IReadOnlyList<ProjectDto>>;

public sealed record ProjectDto(Guid Id, string Code, string Name, string Industry, bool IsArchived);
