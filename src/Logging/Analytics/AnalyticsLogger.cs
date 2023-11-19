using Ayborg.Gateway.Analytics.V1;
using Google.Protobuf.WellKnownTypes;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AyBorg.SDK.Logging.Analytics;

public sealed class AnalyticsLogger : ILogger
{
    private readonly IConfiguration _configuration;
    private readonly IAnalyticsCache _cache;

    public AnalyticsLogger(IConfiguration configuration,
                            IAnalyticsCache analyticsCache)
    {
        _configuration = configuration;
        _cache = analyticsCache;
    }

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull => default!;

    public bool IsEnabled(LogLevel logLevel)
    {
        LogLevel configLogLevel = _configuration.GetValue("Logging:LogLevel:AyBorg.Log", LogLevel.Information);
        return logLevel >= configLogLevel;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        if (!IsEnabled(logLevel)) return;

        _cache.Enqueue(new EventEntry
        {
            ServiceUniqueName = string.Empty,
            Timestamp = Timestamp.FromDateTime(DateTime.UtcNow),
            LogLevel = (int)logLevel,
            EventId = eventId.Id,
            Message = $"{formatter(state, exception)}"
        });
    }
}
