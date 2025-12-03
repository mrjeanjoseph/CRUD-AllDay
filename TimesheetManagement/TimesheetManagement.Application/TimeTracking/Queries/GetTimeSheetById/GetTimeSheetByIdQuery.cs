using TimesheetManagement.Application.Common.Abstractions;

namespace TimesheetManagement.Application.TimeTracking.Queries.GetTimeSheetById;
public sealed record GetTimeSheetByIdQuery(Guid TimeSheetId) : IQuery<TimeSheetDetailsDto>;

public sealed record TimeSheetDetailsDto(
    Guid Id,
    Guid UserId,
    DateOnly From,
    DateOnly To,
    string Status,
    string? Comment,
    IReadOnlyList<TimeEntryDto> Entries);

public sealed record TimeEntryDto(Guid Id, Guid ProjectId, DateOnly Date, decimal Hours, string? Notes);
