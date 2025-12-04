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
            .MustAsync(async (email, ct) =>
            {
                try
                {
                    var emailObj = new TimesheetManagement.Domain.Identity.Email(email);
                    return !(await users.ExistsAsync(emailObj, ct));
                }
                catch
                {
                    return true; // If invalid, don't check existence
                }
            })
            .WithMessage("Email already registered");

        RuleFor(x => x.PasswordHash)
            .NotEmpty().MinimumLength(60);
    }
}
