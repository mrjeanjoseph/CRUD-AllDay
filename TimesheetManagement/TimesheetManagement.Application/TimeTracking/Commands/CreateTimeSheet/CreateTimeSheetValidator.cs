using FluentValidation;
using TimesheetManagement.Domain.TimeTracking.Repositories;

namespace TimesheetManagement.Application.TimeTracking.Commands.CreateTimeSheet;
public sealed class CreateTimeSheetValidator : AbstractValidator<CreateTimeSheetCommand>
{
    public CreateTimeSheetValidator(ITimeSheetRepository repo)
    {
        RuleFor(x => x.UserId).Must(x => x != Guid.Empty);
        RuleFor(x => x.From).NotEmpty();
        RuleFor(x => x.To).NotEmpty().GreaterThanOrEqualTo(x => x.From);

        RuleFor(x => x)
            .MustAsync(async (cmd, ct) => !(await repo.HasSubmittedForRangeAsync(cmd.UserId, cmd.From, cmd.To, ct)))
            .WithMessage("A submitted timesheet already exists for this period");
    }
}
