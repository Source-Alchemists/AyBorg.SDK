using Ayborg.Gateway.Analytics.V1;
using AyBorg.SDK.System.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AyBorg.SDK.Logging.Analytics;

public sealed class AnalyticsBackgroundService : BackgroundService
{
    private readonly ILogger<AnalyticsBackgroundService> _logger;
    private readonly IServiceConfiguration _serviceConfiguration;
    private readonly EventLog.EventLogClient _eventLogClient;
    private readonly AnalyticsCache _cache;

    public AnalyticsBackgroundService(ILogger<AnalyticsBackgroundService> logger,
                                        IServiceConfiguration serviceConfiguration,
                                        EventLog.EventLogClient eventLogClient,
                                        AnalyticsCache analyticsCache)
    {
        _logger = logger;
        _serviceConfiguration = serviceConfiguration;
        _eventLogClient = eventLogClient;
        _cache = analyticsCache;
    }

    public override Task StartAsync(CancellationToken cancellationToken) => base.StartAsync(cancellationToken);

    public override Task StopAsync(CancellationToken cancellationToken) => base.StopAsync(cancellationToken);

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return Task.Factory.StartNew(async () =>
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    if (_cache.TryDequeue(out EventRequest? request))
                    {
                        request.ServiceUniqueName = _serviceConfiguration.UniqueName;
                        await _eventLogClient.LogEventAsync(request);
                    }
                    else
                    {
                        await Task.Delay(100);
                    }
                }
                catch (Exception)
                {
                    await Task.Delay(100);
                }
            }
        });
    }
}
