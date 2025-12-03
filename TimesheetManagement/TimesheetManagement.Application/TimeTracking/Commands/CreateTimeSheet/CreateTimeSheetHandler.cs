using TimesheetManagement.Application.Common.Abstractions;
using TimesheetManagement.Domain.Common.ValueObjects;
using TimesheetManagement.Domain.TimeTracking;
using TimesheetManagement.Domain.TimeTracking.Repositories;

namespace TimesheetManagement.Application.TimeTracking.Commands.CreateTimeSheet;
public sealed class CreateTimeSheetHandler : ICommandHandler<CreateTimeSheetCommand, Guid>
{
    private readonly ITimeSheetRepository _repo;
    private readonly IUnitOfWork _uow;

    public CreateTimeSheetHandler(ITimeSheetRepository repo, IUnitOfWork uow)
    {
        _repo = repo;
        _uow = uow;
    }

    public async Task<Guid> Handle(CreateTimeSheetCommand command, CancellationToken cancellationToken)
    {
        var range = new DateRange(command.From, command.To);
        var overlap = await _repo.HasSubmittedForRangeAsync(command.UserId, range.From, range.To, cancellationToken);
        if (overlap) throw new InvalidOperationException("A submitted timesheet already exists for this period");

        var sheet = new TimeSheet(command.UserId, range.From, range.To);
        await _repo.AddAsync(sheet, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);
        return sheet.Id;
    }
}
