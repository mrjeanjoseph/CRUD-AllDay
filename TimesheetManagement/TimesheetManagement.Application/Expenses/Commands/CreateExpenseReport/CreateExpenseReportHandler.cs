using System;
using System.Threading;
using System.Threading.Tasks;
using TimesheetManagement.Application.Common.Abstractions;
using TimesheetManagement.Domain.Common.ValueObjects;
using TimesheetManagement.Domain.Expenses;
using TimesheetManagement.Domain.Expenses.Repositories;

namespace TimesheetManagement.Application.Expenses.Commands.CreateExpenseReport;
public sealed class CreateExpenseReportHandler : ICommandHandler<CreateExpenseReportCommand, Guid>
{
    private readonly IExpenseReportRepository _repo;
    private readonly IUnitOfWork _uow;

    public CreateExpenseReportHandler(IExpenseReportRepository repo, IUnitOfWork uow)
    {
        _repo = repo;
        _uow = uow;
    }

    public async Task<Guid> Handle(CreateExpenseReportCommand command, CancellationToken cancellationToken)
    {
        var range = new DateRange(command.From, command.To);
        var overlap = await _repo.HasSubmittedForRangeAsync(command.UserId, range.From, range.To, cancellationToken);
        if (overlap) throw new InvalidOperationException("A submitted expense report already exists for this period");

        var report = new ExpenseReport(command.UserId, range.From, range.To);
        await _repo.AddAsync(report, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);
        return report.Id;
    }
}
