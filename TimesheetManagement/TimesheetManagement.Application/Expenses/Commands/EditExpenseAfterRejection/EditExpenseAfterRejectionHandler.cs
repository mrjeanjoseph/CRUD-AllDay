using TimesheetManagement.Application.Common.Abstractions;
using TimesheetManagement.Domain.Expenses.Repositories;

namespace TimesheetManagement.Application.Expenses.Commands.EditExpenseAfterRejection;
public sealed class EditExpenseAfterRejectionHandler : ICommandHandler<EditExpenseAfterRejectionCommand, bool>
{
    private readonly IExpenseReportRepository _repo;
    private readonly IUnitOfWork _uow;

    public EditExpenseAfterRejectionHandler(IExpenseReportRepository repo, IUnitOfWork uow)
    {
        _repo = repo;
        _uow = uow;
    }

    public async Task<bool> Handle(EditExpenseAfterRejectionCommand command, CancellationToken cancellationToken)
    {
        var report = await _repo.GetAsync(command.ExpenseReportId, cancellationToken);
        if (report is null) throw new KeyNotFoundException("Expense report not found");
        report.EditAfterRejection();
        await _repo.UpdateAsync(report, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);
        return true;
    }
}
