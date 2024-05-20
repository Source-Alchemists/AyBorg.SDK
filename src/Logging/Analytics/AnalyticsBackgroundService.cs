using Ayborg.Gateway.Analytics.V1;
using AyBorg.SDK.System.Configuration;
using Microsoft.Extensions.Hosting;

namespace AyBorg.SDK.Logging.Analytics;

public sealed class AnalyticsBackgroundService : BackgroundService
{
    private readonly IServiceConfiguration _serviceConfiguration;
    private readonly IAnalyticsCache _cache;
    private readonly EventLog.EventLogClient _eventLogClient;


    public AnalyticsBackgroundService(IServiceConfiguration serviceConfiguration,
                                        IAnalyticsCache analyticsCache,
                                        EventLog.EventLogClient eventLogClient)
    {
        _serviceConfiguration = serviceConfiguration;
        _cache = analyticsCache;
        _eventLogClient = eventLogClient;
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
                        await _eventLogClient.LogEventAsync(request).ConfigureAwait(false);
                    }
                    else
                    {
                        await Task.Delay(100).ConfigureAwait(false);
                    }
                }
                catch (Exception)
                {
                    await Task.Delay(100).ConfigureAwait(false);
                }
            }
        });
    }
}
