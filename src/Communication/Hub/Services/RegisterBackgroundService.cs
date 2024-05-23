using System.Reflection;
using AyBorg.SDK.Common;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AyBorg.Hub.Connect.Services;

public abstract class RegisterBackgroundService<TClient> : BackgroundService
{
    protected readonly ILogger<RegisterBackgroundService<TClient>> _logger;
    protected readonly TClient _client;
    protected readonly IOptions<HubClientOptions> _options;
    protected readonly string _serviceVersion = string.Empty;

    protected RegisterBackgroundService(ILogger<RegisterBackgroundService<TClient>> logger, TClient client, IOptions<HubClientOptions> options)
    {
        _logger = logger;
        _client = client;
        _options = options;
        AssemblyName assemblyName = Assembly.GetEntryAssembly()!.GetName();
        if (assemblyName.Version != null)
        {
            _serviceVersion = assemblyName.Version.ToString();
        }
    }

    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogTrace((int)EventLogType.Connect, "Registry service starting ...");

        try
        {
            await RegisterAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogWarning((int)EventLogType.Connect, ex, "Failed to register!");
        }

        await base.StartAsync(cancellationToken);
    }
    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogTrace((int)EventLogType.Disconnect, "Registry service stopping ...");
        try
        {
            await UnregisterAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogWarning((int)EventLogType.Disconnect, ex, "Failed to unregister service!");
        }

        await base.StopAsync(cancellationToken);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.CompletedTask;
    }

    protected abstract ValueTask RegisterAsync(CancellationToken cancellationToken);
    protected abstract ValueTask UnregisterAsync(CancellationToken cancellationToken);
}
