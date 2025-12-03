using TimesheetManagement.Application.Common.Abstractions;
using TimesheetManagement.Domain.Expenses.Repositories;

namespace TimesheetManagement.Application.Expenses.Commands.SubmitExpenseReport;
public sealed class SubmitExpenseReportHandler : ICommandHandler<SubmitExpenseReportCommand, bool>
{
    private readonly IExpenseReportRepository _repo;
    private readonly IUnitOfWork _uow;

    public SubmitExpenseReportHandler(IExpenseReportRepository repo, IUnitOfWork uow)
    {
        _repo = repo;
        _uow = uow;
    }

    public async Task<bool> Handle(SubmitExpenseReportCommand command, CancellationToken cancellationToken)
    {
        var report = await _repo.GetAsync(command.ExpenseReportId, cancellationToken);
        if (report is null) throw new KeyNotFoundException("Expense report not found");
        report.Submit();
        await _repo.UpdateAsync(report, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);
        return true;
    }
}
