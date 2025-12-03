using TimesheetManagement.Application.Common.Abstractions;
using TimesheetManagement.Domain.TimeTracking.Repositories;

namespace TimesheetManagement.Application.TimeTracking.Queries.GetTimeSheetById;
public sealed class GetTimeSheetByIdHandler : IQueryHandler<GetTimeSheetByIdQuery, TimeSheetDetailsDto>
{
    private readonly ITimeSheetRepository _repo;

    public GetTimeSheetByIdHandler(ITimeSheetRepository repo)
    {
        _repo = repo;
    }

    public async Task<TimeSheetDetailsDto> Handle(GetTimeSheetByIdQuery query, CancellationToken cancellationToken)
    {
        var s = await _repo.GetAsync(query.TimeSheetId, cancellationToken);
        if (s is null) throw new KeyNotFoundException("Timesheet not found");
        return new TimeSheetDetailsDto(
            s.Id,
            s.UserId,
            s.Period.From,
            s.Period.To,
            s.Status.ToString(),
            s.Comment,
            s.Entries.Select(e => new TimeEntryDto(e.Id, e.ProjectId, e.Date, e.Hours.Value, e.Notes)).ToList()
        );
    }
}
