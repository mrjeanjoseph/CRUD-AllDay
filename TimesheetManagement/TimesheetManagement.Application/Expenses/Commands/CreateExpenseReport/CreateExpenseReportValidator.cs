using FluentValidation;
using TimesheetManagement.Domain.Expenses.Repositories;

namespace TimesheetManagement.Application.Expenses.Commands.CreateExpenseReport;
public sealed class CreateExpenseReportValidator : AbstractValidator<CreateExpenseReportCommand>
{
    public CreateExpenseReportValidator(IExpenseReportRepository repo)
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.From).NotEmpty();
        RuleFor(x => x.To).NotEmpty().GreaterThanOrEqualTo(x => x.From);

        RuleFor(x => x)
            .MustAsync(async (cmd, ct) => !(await repo.HasSubmittedForRangeAsync(cmd.UserId, cmd.From, cmd.To, ct)))
            .WithMessage("A submitted expense report already exists for this period");
    }
}
