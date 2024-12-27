using Microsoft.Extensions.Options;

namespace AdventureWorks.ServiceAPI.Logging;

[ProviderAlias("AdWDataLogs")]
public class AdWFileLoggerProvider : ILoggerProvider {
    public readonly AdWFileLoggerOptions _options;
    public AdWFileLoggerProvider(IOptions<AdWFileLoggerOptions> fileOptions) {

        _options = fileOptions.Value;

        if (!Directory.Exists(_options.FolderPath))
            Directory.CreateDirectory(_options.FolderPath);            
    }

    public ILogger CreateLogger(string categoryName) {
        return new AdWFileLogger(this);
    }

    public void Dispose() { }
}
