using System;
using TimesheetManagement.Application.Common.Abstractions;

namespace TimesheetManagement.Application.Projects.Commands.RestoreProject;
public sealed record RestoreProjectCommand(Guid ProjectId) : ICommand<bool>;
