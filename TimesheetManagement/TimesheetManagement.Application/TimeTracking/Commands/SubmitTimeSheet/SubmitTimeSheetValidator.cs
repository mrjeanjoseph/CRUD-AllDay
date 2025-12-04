using FluentValidation;
using TimesheetManagement.Domain.TimeTracking;
using TimesheetManagement.Domain.TimeTracking.Repositories;

namespace TimesheetManagement.Application.TimeTracking.Commands.SubmitTimeSheet;

public sealed class SubmitTimeSheetValidator : AbstractValidator<SubmitTimeSheetCommand>
{
    public SubmitTimeSheetValidator(ITimeSheetRepository repo)
    {
        RuleFor(x => x.TimeSheetId).Must(x => x != Guid.Empty);

        RuleFor(x => x.TimeSheetId)
            .MustAsync(async (id, ct) =>
            {
                var s = await repo.GetAsync(id, ct);
                return s is not null && s.Status == TimeSheetStatus.Draft && s.Entries.Count > 0;
            })
            .WithMessage("Timesheet must be draft and contain at least one entry");
    }
}
