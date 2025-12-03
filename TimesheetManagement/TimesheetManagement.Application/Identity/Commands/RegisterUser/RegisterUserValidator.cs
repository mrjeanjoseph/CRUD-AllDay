using FluentValidation;
using TimesheetManagement.Domain.Identity.Repositories;

namespace TimesheetManagement.Application.Identity.Commands.RegisterUser;
public sealed class RegisterUserValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserValidator(IUserRepository users)
    {
        RuleFor(x => x.Username)
            .NotEmpty().MinimumLength(3);

        RuleFor(x => x.Email)
            .NotEmpty().EmailAddress();

        RuleFor(x => x.Email)
            .MustAsync(async (email, ct) => !(await users.ExistsAsync(new TimesheetManagement.Domain.Identity.Email(email), ct)))
            .WithMessage("Email already registered");

        RuleFor(x => x.PasswordHash)
            .NotEmpty().MinimumLength(60);
    }
}
