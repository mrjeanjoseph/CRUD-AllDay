using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TimesheetManagement.Application.Common.Abstractions;
using TimesheetManagement.Domain.TimeTracking.Repositories;

namespace TimesheetManagement.Application.TimeTracking.Commands.ApproveTimeSheet;
public sealed class ApproveTimeSheetHandler : ICommandHandler<ApproveTimeSheetCommand, bool>
{
    private readonly ITimeSheetRepository _repo;
    private readonly IUnitOfWork _uow;

    public ApproveTimeSheetHandler(ITimeSheetRepository repo, IUnitOfWork uow)
    {
        _repo = repo;
        _uow = uow;
    }

    public async Task<bool> Handle(ApproveTimeSheetCommand command, CancellationToken cancellationToken)
    {
        var sheet = await _repo.GetAsync(command.TimeSheetId, cancellationToken);
        if (sheet is null) throw new KeyNotFoundException("Timesheet not found");
        sheet.Approve(command.Comment);
        await _repo.UpdateAsync(sheet, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);
        return true;
    }
}
