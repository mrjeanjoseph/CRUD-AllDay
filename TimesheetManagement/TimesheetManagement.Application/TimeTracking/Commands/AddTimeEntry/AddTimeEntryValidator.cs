using FluentValidation;
using TimesheetManagement.Domain.TimeTracking.Repositories;

namespace TimesheetManagement.Application.TimeTracking.Commands.AddTimeEntry;
public sealed class AddTimeEntryValidator : AbstractValidator<AddTimeEntryCommand>
{
    public AddTimeEntryValidator(ITimeSheetRepository repo)
    {
        RuleFor(x => x.ProjectId).NotEmpty();
        RuleFor(x => x.Date).NotEmpty();
        RuleFor(x => x.Hours).GreaterThan(0).LessThanOrEqualTo(24);

        RuleFor(x => x)
            .MustAsync(async (cmd, ct) =>
            {
                var sheet = await repo.GetAsync(cmd.TimeSheetId, ct);
                return sheet is not null && sheet.Status == TimesheetManagement.Domain.TimeTracking.TimeSheetStatus.Draft && sheet.Period.Contains(cmd.Date);
            })
            .WithMessage("Timesheet must be draft and date within its period");
    }
}
