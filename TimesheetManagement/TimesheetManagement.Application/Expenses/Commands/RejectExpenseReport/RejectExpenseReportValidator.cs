using FluentValidation;
using TimesheetManagement.Application.Common.Abstractions;
using TimesheetManagement.Domain.Expenses.Repositories;

namespace TimesheetManagement.Application.Expenses.Commands.RejectExpenseReport;
public sealed class RejectExpenseReportValidator : AbstractValidator<RejectExpenseReportCommand>
{
    public RejectExpenseReportValidator(IExpenseReportRepository repo, IUserContext context)
    {
        RuleFor(x => x.ExpenseReportId).NotEmpty();
        RuleFor(x => x.Comment).NotEmpty();

        RuleFor(x => x)
            .Must(_ => context.IsInRole("Admin") || context.IsInRole("SuperAdmin"))
            .WithMessage("Only Admin or SuperAdmin can reject");

        RuleFor(x => x.ExpenseReportId)
            .MustAsync(async (id, ct) =>
            {
                var r = await repo.GetAsync(id, ct);
                return r is not null && r.Status == TimesheetManagement.Domain.Expenses.ExpenseStatus.Submitted;
            })
            .WithMessage("Expense report must be submitted to reject");
    }
}
