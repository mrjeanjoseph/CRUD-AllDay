using FluentValidation;
using TimesheetManagement.Application.Common.Abstractions;
using TimesheetManagement.Domain.TimeTracking.Repositories;

namespace TimesheetManagement.Application.TimeTracking.Commands.RejectTimeSheet;

public sealed class RejectTimeSheetValidator : AbstractValidator<RejectTimeSheetCommand>
{
    public RejectTimeSheetValidator(ITimeSheetRepository repo, IUserContext context)
    {
        RuleFor(x => x.TimeSheetId).NotEmpty();
        RuleFor(x => x.Comment).NotEmpty();

        RuleFor(x => x)
            .Must(_ => context.IsInRole("Admin") || context.IsInRole("SuperAdmin"))
            .WithMessage("Only Admin or SuperAdmin can reject");

        RuleFor(x => x.TimeSheetId)
            .MustAsync(async (id, ct) =>
            {
                var s = await repo.GetAsync(id, ct);
                return s is not null && s.Status == TimesheetManagement.Domain.TimeTracking.TimeSheetStatus.Submitted;
            })
            .WithMessage("Timesheet must be submitted to reject");
    }
}
