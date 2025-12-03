using TimesheetManagement.Application.Common.Abstractions;

namespace TimesheetManagement.Application.Projects.Commands.CreateProject;
public sealed record CreateProjectCommand(string Code, string Name, string Industry) : ICommand<Guid>;
