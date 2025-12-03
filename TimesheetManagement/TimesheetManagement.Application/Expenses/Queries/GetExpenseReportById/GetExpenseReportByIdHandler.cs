using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TimesheetManagement.Application.Common.Abstractions;
using TimesheetManagement.Domain.Expenses.Repositories;

namespace TimesheetManagement.Application.Expenses.Queries.GetExpenseReportById;

public sealed class GetExpenseReportByIdHandler : IQueryHandler<GetExpenseReportByIdQuery, ExpenseReportDetailsDto>
{
    private readonly IExpenseReportRepository _repo;

    public GetExpenseReportByIdHandler(IExpenseReportRepository repo)
    {
        _repo = repo;
    }

    public async Task<ExpenseReportDetailsDto> Handle(GetExpenseReportByIdQuery query, CancellationToken cancellationToken)
    {
        var r = await _repo.GetAsync(query.ExpenseReportId, cancellationToken);
        if (r is null) throw new KeyNotFoundException("Expense report not found");
        return new ExpenseReportDetailsDto(
            r.Id,
            r.UserId,
            r.Period.From,
            r.Period.To,
            r.Status.ToString(),
            r.Comment,
            r.Items.Select(i => new ExpenseItemDto(i.Id, i.Date, i.Category, i.Amount.Amount, i.Amount.Currency, i.ReceiptPath, i.Notes)).ToList()
        );
    }
}
