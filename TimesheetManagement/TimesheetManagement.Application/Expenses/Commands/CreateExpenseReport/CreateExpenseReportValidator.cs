using FluentValidation;
using TimesheetManagement.Domain.Expenses.Repositories;

namespace TimesheetManagement.Application.Expenses.Commands.CreateExpenseReport;
public sealed class CreateExpenseReportValidator : AbstractValidator<CreateExpenseReportCommand>
{
    public CreateExpenseReportValidator(IExpenseReportRepository repo)
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.From).NotEmpty();
        RuleFor(x => x.From)
            .LessThanOrEqualTo(x => x.To)
            .WithMessage("From must be less than or equal to To");

        RuleFor(x => x.To).NotEmpty();
        RuleFor(x => x.To).GreaterThanOrEqualTo(x => x.From).WithMessage("To must be greater than or equal to From");

        RuleFor(x => x)
            .MustAsync(async (cmd, ct) => !(await repo.HasSubmittedForRangeAsync(cmd.UserId, cmd.From, cmd.To, ct)))
            .WithMessage("A submitted expense report already exists for this period");
    }
}
