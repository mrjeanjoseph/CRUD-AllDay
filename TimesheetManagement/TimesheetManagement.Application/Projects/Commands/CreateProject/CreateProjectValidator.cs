using FluentValidation;
using TimesheetManagement.Domain.Projects.Repositories;

namespace TimesheetManagement.Application.Projects.Commands.CreateProject;
public sealed class CreateProjectValidator : AbstractValidator<CreateProjectCommand>
{
    public CreateProjectValidator(IProjectRepository projects)
    {
        RuleFor(x => x.Code).NotEmpty().MaximumLength(64);
        RuleFor(x => x.Name).NotEmpty().MaximumLength(128);
        RuleFor(x => x.Industry).NotEmpty().MaximumLength(128);

        RuleFor(x => x.Code)
            .MustAsync(async (code, ct) => !(await projects.CodeExistsAsync(code, ct)))
            .WithMessage("Project code already exists");

        RuleFor(x => x.Name)
            .MustAsync(async (name, ct) => !(await projects.NameExistsAsync(name, ct)))
            .WithMessage("Project name already exists");
    }
}
