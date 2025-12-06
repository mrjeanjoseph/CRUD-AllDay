using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TimesheetManagement.Application.Common.Abstractions;
using FluentValidation;

namespace TimesheetManagement.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        // Register all command handlers
        services.Scan(scan => scan
            .FromAssemblies(assembly)
            .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        // Register all query handlers
        services.Scan(scan => scan
            .FromAssemblies(assembly)
            .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        // Register FluentValidation validators
        services.AddValidatorsFromAssembly(assembly);

        // TODO: Register domain event dispatcher implementation when created
        // services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();

        return services;
    }
}