using TimesheetManagement.Application.Common.Abstractions;

namespace TimesheetManagement.Application.Teams.Commands.ArchiveTeam;
public sealed record ArchiveTeamCommand(Guid TeamId) : ICommand<bool>;
