using System.Diagnostics.CodeAnalysis;

namespace AdventureWorks.ServiceAPI.Logging;
public class AdWFileLogger : ILogger {

    public readonly AdWFileLoggerProvider _fileLogProvider;
    public AdWFileLogger([NotNull] AdWFileLoggerProvider fileLogProvider) {
        _fileLogProvider = fileLogProvider;
    }
    public IDisposable? BeginScope<TState>(TState state) where TState : notnull {
        return null;
    }

    public bool IsEnabled(LogLevel logLevel) {
        return logLevel != LogLevel.None;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state,
        Exception? exception, Func<TState, Exception?, string> formatter) {

        if (!IsEnabled(logLevel))
            return;

        var concatFilePath = string.Format(string.Concat(_fileLogProvider._options.FolderPath, "/",
            _fileLogProvider._options.FileName.Replace("{date}", DateTime.UtcNow.ToString("yyyyMMdd"))));

        var fullFilePath = string.Format("{0}/{1}", _fileLogProvider._options.FolderPath,
            _fileLogProvider._options.FileName.Replace("{date}", DateTime.UtcNow.ToString("yyyyMMdd")));
        var logRecord = string.Format("{0} [{1}] {2} {3}", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"),
            logLevel.ToString(), formatter(state, exception), (exception != null ? exception.StackTrace : ""));

        using (var streamwriter = new StreamWriter(fullFilePath, true)) {
            streamwriter.WriteLine(logRecord);
        }
    }
}