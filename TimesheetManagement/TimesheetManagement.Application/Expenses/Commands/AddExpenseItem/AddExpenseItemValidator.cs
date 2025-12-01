using FluentValidation;
using TimesheetManagement.Domain.Expenses.Repositories;

namespace TimesheetManagement.Application.Expenses.Commands.AddExpenseItem;
public sealed class AddExpenseItemValidator : AbstractValidator<AddExpenseItemCommand>
{
    public AddExpenseItemValidator(IExpenseReportRepository repo)
    {
        RuleFor(x => x.Category).NotEmpty().MaximumLength(128);
        RuleFor(x => x.Amount).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Currency).NotEmpty().Length(3);

        RuleFor(x => x)
            .MustAsync(async (cmd, ct) =>
            {
                var report = await repo.GetAsync(cmd.ExpenseReportId, ct);
                return report is not null && report.Status == TimesheetManagement.Domain.Expenses.ExpenseStatus.Draft && report.Period.Contains(cmd.Date);
            })
            .WithMessage("Expense report must be draft and date within its period");
    }
}
