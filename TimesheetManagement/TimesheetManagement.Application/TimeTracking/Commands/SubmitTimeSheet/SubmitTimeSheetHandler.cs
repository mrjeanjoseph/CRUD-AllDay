namespace TimesheetManagement.Application.TimeTracking.Commands.SubmitTimeSheet;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TimesheetManagement.Application.Common.Abstractions;
using TimesheetManagement.Domain.TimeTracking.Repositories;

public sealed class SubmitTimeSheetHandler : ICommandHandler<SubmitTimeSheetCommand, bool>
{
    private readonly ITimeSheetRepository _repo;
    private readonly IUnitOfWork _uow;

    public SubmitTimeSheetHandler(ITimeSheetRepository repo, IUnitOfWork uow)
    {
        _repo = repo;
        _uow = uow;
    }

    public async Task<bool> Handle(SubmitTimeSheetCommand command, CancellationToken cancellationToken)
    {
        var sheet = await _repo.GetAsync(command.TimeSheetId, cancellationToken);
        if (sheet is null) throw new KeyNotFoundException("Timesheet not found");
        sheet.Submit();
        await _repo.UpdateAsync(sheet, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);
        return true;
    }
}
