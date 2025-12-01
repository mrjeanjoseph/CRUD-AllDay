using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TimesheetManagement.Application.Common.Abstractions;
using TimesheetManagement.Domain.TimeTracking;
using TimesheetManagement.Domain.TimeTracking.Repositories;
using TimesheetManagement.Domain.TimeTracking.ValueObjects;

namespace TimesheetManagement.Application.TimeTracking.Commands.AddTimeEntry;
public sealed class AddTimeEntryHandler : ICommandHandler<AddTimeEntryCommand, bool>
{
    private readonly ITimeSheetRepository _repo;
    private readonly IUnitOfWork _uow;

    public AddTimeEntryHandler(ITimeSheetRepository repo, IUnitOfWork uow)
    {
        _repo = repo;
        _uow = uow;
    }

    public async Task<bool> Handle(AddTimeEntryCommand command, CancellationToken cancellationToken)
    {
        var sheet = await _repo.GetAsync(command.TimeSheetId, cancellationToken);
        if (sheet is null) throw new KeyNotFoundException("Timesheet not found");
        if (sheet.Status != TimeSheetStatus.Draft) throw new InvalidOperationException("Cannot add entry unless timesheet is draft");

        var entry = new TimeEntry(command.ProjectId, DateOnly.FromDateTime(command.Date.ToDateTime(TimeOnly.MinValue)), new HoursWorked(command.Hours), command.Notes);
        sheet.AddEntry(entry);
        await _repo.UpdateAsync(sheet, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);
        return true;
    }
}
