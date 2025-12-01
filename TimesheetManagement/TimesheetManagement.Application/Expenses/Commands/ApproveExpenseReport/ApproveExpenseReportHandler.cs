using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TimesheetManagement.Application.Common.Abstractions;
using TimesheetManagement.Domain.Expenses.Repositories;

namespace TimesheetManagement.Application.Expenses.Commands.ApproveExpenseReport;
public sealed class ApproveExpenseReportHandler : ICommandHandler<ApproveExpenseReportCommand, bool>
{
    private readonly IExpenseReportRepository _repo;
    private readonly IUnitOfWork _uow;

    public ApproveExpenseReportHandler(IExpenseReportRepository repo, IUnitOfWork uow)
    {
        _repo = repo;
        _uow = uow;
    }

    public async Task<bool> Handle(ApproveExpenseReportCommand command, CancellationToken cancellationToken)
    {
        var report = await _repo.GetAsync(command.ExpenseReportId, cancellationToken);
        if (report is null) throw new KeyNotFoundException("Expense report not found");
        report.Approve(command.Comment);
        await _repo.UpdateAsync(report, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);
        return true;
    }
}
