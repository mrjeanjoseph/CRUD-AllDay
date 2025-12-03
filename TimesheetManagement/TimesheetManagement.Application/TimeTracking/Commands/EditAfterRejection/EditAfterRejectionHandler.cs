using TimesheetManagement.Application.Common.Abstractions;
using TimesheetManagement.Domain.TimeTracking.Repositories;

namespace TimesheetManagement.Application.TimeTracking.Commands.EditAfterRejection;
public sealed class EditAfterRejectionHandler : ICommandHandler<EditAfterRejectionCommand, bool>
{
    private readonly ITimeSheetRepository _repo;
    private readonly IUnitOfWork _uow;

    public EditAfterRejectionHandler(ITimeSheetRepository repo, IUnitOfWork uow)
    {
        _repo = repo;
        _uow = uow;
    }

    public async Task<bool> Handle(EditAfterRejectionCommand command, CancellationToken cancellationToken)
    {
        var sheet = await _repo.GetAsync(command.TimeSheetId, cancellationToken);
        if (sheet is null) throw new KeyNotFoundException("Timesheet not found");
        sheet.EditAfterRejection();
        await _repo.UpdateAsync(sheet, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);
        return true;
    }
}
