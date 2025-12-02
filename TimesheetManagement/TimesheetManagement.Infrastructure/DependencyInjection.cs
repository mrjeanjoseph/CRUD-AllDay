using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TimesheetManagement.Application.Common.Abstractions;
using TimesheetManagement.Domain.Expenses.Repositories;
using TimesheetManagement.Domain.Identity.Repositories;
using TimesheetManagement.Domain.Projects.Repositories;
using TimesheetManagement.Domain.Teams.Repositories;
using TimesheetManagement.Domain.TimeTracking.Repositories;
using TimesheetManagement.Infrastructure.Persistence;
using TimesheetManagement.Infrastructure.Repositories;

namespace TimesheetManagement.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            // Use existing connection string name from appsettings
            options.UseSqlServer(configuration.GetConnectionString("TimesheetDBEntities"));
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleAssignmentRepository, RoleAssignmentRepository>();
        services.AddScoped<ITimeSheetRepository, TimeSheetRepository>();
        services.AddScoped<IExpenseReportRepository, ExpenseReportRepository>();
        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<ITeamRepository, TeamRepository>();

        return services;
    }
}
