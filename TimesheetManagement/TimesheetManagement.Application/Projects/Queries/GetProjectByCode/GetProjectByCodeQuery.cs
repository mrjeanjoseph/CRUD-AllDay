using TimesheetManagement.Application.Common.Abstractions;
using TimesheetManagement.Application.Projects.Queries.GetAllProjects;

namespace TimesheetManagement.Application.Projects.Queries.GetProjectByCode;
public sealed record GetProjectByCodeQuery(string Code) : IQuery<ProjectDto>;
