using FluentValidation;
using TimesheetManagement.Application.Common.Abstractions;
using TimesheetManagement.Domain.Identity.Repositories;

namespace TimesheetManagement.Application.Identity.Commands.AssignRole;
public sealed class AssignRoleValidator : AbstractValidator<AssignRoleCommand>
{
    public AssignRoleValidator(IUserRepository users, IUserContext context)
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.Role).IsInEnum();

        RuleFor(x => x)
            .Must(_ => context.IsInRole("Admin") || context.IsInRole("SuperAdmin"))
            .WithMessage("Only Admin or SuperAdmin can assign roles");

        RuleFor(x => x.UserId)
            .MustAsync(async (userId, ct) => (await users.GetAsync(userId, ct)) is not null)
            .WithMessage("User not found");
    }
}
