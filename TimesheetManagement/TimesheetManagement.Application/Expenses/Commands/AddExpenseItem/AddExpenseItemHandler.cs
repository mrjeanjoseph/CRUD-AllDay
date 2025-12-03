using TimesheetManagement.Application.Common.Abstractions;
using TimesheetManagement.Domain.Expenses;
using TimesheetManagement.Domain.Expenses.Repositories;
using TimesheetManagement.Domain.Expenses.ValueObjects;

namespace TimesheetManagement.Application.Expenses.Commands.AddExpenseItem;
public sealed class AddExpenseItemHandler : ICommandHandler<AddExpenseItemCommand, bool>
{
    private readonly IExpenseReportRepository _repo;
    private readonly IUnitOfWork _uow;

    public AddExpenseItemHandler(IExpenseReportRepository repo, IUnitOfWork uow)
    {
        _repo = repo;
        _uow = uow;
    }

    public async Task<bool> Handle(AddExpenseItemCommand command, CancellationToken cancellationToken)
    {
        var report = await _repo.GetAsync(command.ExpenseReportId, cancellationToken);
        if (report is null) throw new KeyNotFoundException("Expense report not found");

        var item = new ExpenseItem(command.Date, command.Category, new Money(command.Amount, command.Currency), command.ReceiptPath, command.Notes);
        report.AddItem(item);
        await _repo.UpdateAsync(report, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);
        return true;
    }
}
