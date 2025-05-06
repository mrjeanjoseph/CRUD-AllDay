using AdventureWorks.Domain.DataAccessLayer;
using Microsoft.Extensions.DependencyInjection;

namespace AdventureWorks.Domain.Events;

public static class AdvWDB_HealthCheck
{
    private static readonly string appName = AppDomain.CurrentDomain.FriendlyName.Split('.')[0];

    public static async Task<bool> CheckDatabaseAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AdWDbContext>();
        try
        {
            await dbContext.Database.CanConnectAsync();
            LogToFile($"{appName} Database health check successful.");
            return true;
        }
        catch (Exception ex)
        {
            LogToFile($"{appName} Database health check failed with the following errors: {ex.Message}");
            return false;
        }
    }

    private static void LogToFile(string message)
    { 
        var logDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), appName, "Logs");
        Directory.CreateDirectory(logDirectory);
        var logFilePath = Path.Combine(logDirectory, "log.txt");
        File.AppendAllText(logFilePath, $"{DateTime.Now}: {message}{Environment.NewLine}");
    }
}
