using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AyBorg.SDK.Logging.Analytics;

[ProviderAlias("AyBorg.Analytics")]
public sealed class AnalyticsLoggerProvider : ILoggerProvider
{
    private readonly IConfiguration _configuration;
    private readonly IAnalyticsCache _cache;
    private AnalyticsLogger _logger = null!;
    private bool _isDisposed = false;

    public AnalyticsLoggerProvider(IConfiguration configuration,
                                    IAnalyticsCache analyticsCache)
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
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool isDisposing)
    {
        if (isDisposing && !_isDisposed)
        {
            _logger = null!;
            _isDisposed = true;
        }
    }
}
