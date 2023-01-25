using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AyBorg.SDK.Logging.Analytics;

[ProviderAlias("AyBorg.Analytics")]
public sealed class AnalyticsLoggerProvider : ILoggerProvider
{
    private readonly IConfiguration _configuration;
    private readonly AnalyticsCache _cache;
    private AnalyticsLogger _logger = null!;

    public AnalyticsLoggerProvider(IConfiguration configuration,
                                    AnalyticsCache analyticsCache)
    {
        _configuration = configuration;
        _cache = analyticsCache;
    }

    public ILogger CreateLogger(string categoryName)
    {
        _logger ??= new AnalyticsLogger(_configuration, _cache);
        return _logger;
    }

    public void Dispose()
    {
    }
}
