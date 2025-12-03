using FluentValidation;
using TimesheetManagement.Application.Common.Abstractions;
using TimesheetManagement.Domain.TimeTracking;
using TimesheetManagement.Domain.TimeTracking.Repositories;

namespace TimesheetManagement.Application.TimeTracking.Commands.ApproveTimeSheet;

public sealed class ApproveTimeSheetValidator : AbstractValidator<ApproveTimeSheetCommand>
{
    public ApproveTimeSheetValidator(ITimeSheetRepository repo, IUserContext context)
    {
        RuleFor(x => x.TimeSheetId).NotEmpty();

        RuleFor(x => x)
            .Must(_ => context.IsInRole("Admin") || context.IsInRole("SuperAdmin"))
            .WithMessage("Only Admin or SuperAdmin can approve");

        RuleFor(x => x.TimeSheetId)
            .MustAsync(async (id, ct) =>
            {
                var s = await repo.GetAsync(id, ct);
                return s is not null && s.Status == TimeSheetStatus.Submitted;
            })
            .WithMessage("Timesheet must be submitted to approve");
    }
}
