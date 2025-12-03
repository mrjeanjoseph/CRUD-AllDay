using TimesheetManagement.Application.Common.Abstractions;
using TimesheetManagement.Domain.TimeTracking.Repositories;

namespace TimesheetManagement.Application.TimeTracking.Queries.GetTimeSheetsForUser;
public sealed class GetTimeSheetsForUserHandler : IQueryHandler<GetTimeSheetsForUserQuery, IReadOnlyList<TimeSheetSummaryDto>>
{
    private readonly ITimeSheetRepository _repo;

    public GetTimeSheetsForUserHandler(ITimeSheetRepository repo)
    {
        _repo = repo;
    }

    public async Task<IReadOnlyList<TimeSheetSummaryDto>> Handle(GetTimeSheetsForUserQuery query, CancellationToken cancellationToken)
    {
        var sheets = await _repo.GetForUserAsync(query.UserId, query.From, query.To, cancellationToken);
        return sheets.Select(s => new TimeSheetSummaryDto(
            s.Id,
            s.Period.From,
            s.Period.To,
            s.Status.ToString(),
            s.Entries.Count
        )).ToList();
    }
}
