using TimesheetManagement.Application.Common.Abstractions;
using TimesheetManagement.Domain.Expenses.Repositories;

namespace TimesheetManagement.Application.Expenses.Commands.RejectExpenseReport;
public sealed class RejectExpenseReportHandler : ICommandHandler<RejectExpenseReportCommand, bool>
{
    private readonly IExpenseReportRepository _repo;
    private readonly IUnitOfWork _uow;

    public RejectExpenseReportHandler(IExpenseReportRepository repo, IUnitOfWork uow)
    {
        _repo = repo;
        _uow = uow;
    }

    public async Task<bool> Handle(RejectExpenseReportCommand command, CancellationToken cancellationToken)
    {
        var report = await _repo.GetAsync(command.ExpenseReportId, cancellationToken);
        if (report is null) throw new KeyNotFoundException("Expense report not found");
        report.Reject(command.Comment);
        await _repo.UpdateAsync(report, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);
        return true;
    }
}
