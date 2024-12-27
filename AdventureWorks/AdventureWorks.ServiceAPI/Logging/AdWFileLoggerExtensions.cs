namespace AdventureWorks.ServiceAPI.Logging;

public static class AdWFileLoggerExtensions {
    public static ILoggingBuilder AddAdWFileLogger(this ILoggingBuilder builder, Action<AdWFileLoggerOptions> configOpt) {
        
        builder.Services.AddSingleton<ILoggerProvider, AdWFileLoggerProvider>();

        builder.Services.Configure(configOpt);

        return builder;
    }
}
