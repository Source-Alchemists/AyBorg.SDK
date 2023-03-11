using Ayborg.Gateway.Analytics.V1;
using AyBorg.SDK.System.Configuration;
using Microsoft.Extensions.Hosting;

namespace AyBorg.SDK.Logging.Analytics;

public sealed class AnalyticsBackgroundService : BackgroundService
{
    private readonly IServiceConfiguration _serviceConfiguration;
    private readonly EventLog.EventLogClient _eventLogClient;
    private readonly AnalyticsCache _cache;

    public AnalyticsBackgroundService(IServiceConfiguration serviceConfiguration,
                                        EventLog.EventLogClient eventLogClient,
                                        AnalyticsCache analyticsCache)
    {
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
                    if (_cache.TryDequeue(out EventEntry? request))
                    {
                        request.ServiceType = _serviceConfiguration.TypeName;
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
