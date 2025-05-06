using AdventureWorks.Domain.DataAccessLayer;
using AdventureWorks.Domain.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using static System.Console;

namespace AdventureWorks.ConsoleApp;

internal class Program
{
    private static void Main(string[] args)
    {
        WriteLine("Welcome to Adventure works");
        var services = new ServiceCollection();

        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();

        var connectionString = configuration.GetConnectionString("AdWConn");
        var getEnvVar = Environment.GetEnvironmentVariable("AdWConn");

        WriteLine($"Connection String: {connectionString ?? "Not Found"}");
        WriteLine($"Environment Connection String: {getEnvVar}");
        // For some reason, even though the connection string is not found,
        // it still connects to the DB successfully

        services.AddDbContext<AdWDbContext>(options =>
            options.UseSqlServer(connectionString));

        var serviceProvider = services.BuildServiceProvider();
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AdWDbContext>();

        bool isHealthy = AdvWDB_HealthCheck.CheckDatabaseAsync(serviceProvider).Result;

        WriteLine($"Database Health: {(isHealthy ? "Healthy" : "Unhealthy")}");
    }
}
