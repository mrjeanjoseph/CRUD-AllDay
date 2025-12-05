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
                // If format is invalid, skip uniqueness check (other rule will flag format)
                if (string.IsNullOrWhiteSpace(email) || !email.Contains('@'))
                    return true;

                try
                {
                    var emailObj = new TimesheetManagement.Domain.Identity.Email(email);
                    return !(await users.ExistsAsync(emailObj, ct));
                }
                catch
                {
                    // If Email ctor throws, let the format rule handle it - do not throw from validator
                    return true;
                }
            })
            .WithMessage("Email already registered");

        RuleFor(x => x.PasswordHash)
            .NotEmpty().MinimumLength(60);
    }
}
