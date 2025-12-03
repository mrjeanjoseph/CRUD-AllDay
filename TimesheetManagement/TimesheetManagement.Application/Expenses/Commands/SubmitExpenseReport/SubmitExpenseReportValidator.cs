using FluentValidation;
using TimesheetManagement.Domain.Expenses.Repositories;

namespace TimesheetManagement.Application.Expenses.Commands.SubmitExpenseReport;
public sealed class SubmitExpenseReportValidator : AbstractValidator<SubmitExpenseReportCommand>
{
    public SubmitExpenseReportValidator(IExpenseReportRepository repo)
    {
        RuleFor(x => x.ExpenseReportId).NotEmpty();

        RuleFor(x => x.ExpenseReportId)
            .MustAsync(async (id, ct) =>
            {
                var r = await repo.GetAsync(id, ct);
                return r is not null && r.Status == TimesheetManagement.Domain.Expenses.ExpenseStatus.Draft && r.Items.Count > 0;
            })
            .WithMessage("Expense report must be draft and contain at least one item");
    }
}
