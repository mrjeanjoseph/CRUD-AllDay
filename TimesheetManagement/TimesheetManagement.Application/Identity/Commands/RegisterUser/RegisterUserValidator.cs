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
            .NotEmpty()
            .Must(email => !string.IsNullOrWhiteSpace(email) && email.Contains('@'))
            .WithMessage("Invalid email format");

        RuleFor(x => x.Email)
            .MustAsync(async (email, ct) =>
            {
                var emailObj = new TimesheetManagement.Domain.Identity.Email(email);
                return !(await users.ExistsAsync(emailObj, ct));
            })
            .WithMessage("Email already registered");

        RuleFor(x => x.PasswordHash)
            .NotEmpty().MinimumLength(60);
    }
}
