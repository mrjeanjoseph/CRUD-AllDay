using FluentValidation;
using TimesheetManagement.Application.Common.Abstractions;
using TimesheetManagement.Domain.Identity.Repositories;

namespace TimesheetManagement.Application.Identity.Commands.ChangePassword;

public sealed class ChangePasswordValidator : AbstractValidator<ChangePasswordCommand>
{
    public ChangePasswordValidator(IUserRepository users, IUserContext context)
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.NewPasswordHash).NotEmpty().MinimumLength(60);

        RuleFor(x => x)
            .Must(cmd => context.UserId == cmd.UserId || context.IsInRole("Admin") || context.IsInRole("SuperAdmin"))
            .WithMessage("Not authorized to change this password");

        RuleFor(x => x.UserId)
            .MustAsync(async (userId, ct) => (await users.GetAsync(userId, ct)) is not null)
            .WithMessage("User not found");
    }
}
