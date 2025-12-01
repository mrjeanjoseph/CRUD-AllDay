namespace TimesheetManagement.Application.Expenses.Queries.GetExpenseReportsForUser;

using TimesheetManagement.Application.Common.Abstractions;
using TimesheetManagement.Domain.Expenses.Repositories;

public sealed class GetExpenseReportsForUserHandler : IQueryHandler<GetExpenseReportsForUserQuery, IReadOnlyList<ExpenseReportSummaryDto>>
{
    private readonly IExpenseReportRepository _repo;

    public GetExpenseReportsForUserHandler(IExpenseReportRepository repo)
    {
        _repo = repo;
    }

    public async Task<IReadOnlyList<ExpenseReportSummaryDto>> Handle(GetExpenseReportsForUserQuery query, CancellationToken cancellationToken)
    {
        var reports = await _repo.GetForUserAsync(query.UserId, query.From, query.To, cancellationToken);
        return reports.Select(r => new ExpenseReportSummaryDto(
            r.Id,
            r.Period.From,
            r.Period.To,
            r.Status.ToString(),
            r.Items.Count
        )).ToList();
    }
}
