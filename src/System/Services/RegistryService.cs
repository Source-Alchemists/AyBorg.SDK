using Sys = System;
using System.Reflection;
using System.Text;
using System.Text.Json;
using Atomy.SDK.Data.DTOs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Atomy.SDK.System.Services;

public class RegistryService : BackgroundService
{
    private readonly ILogger<RegistryService> _logger;
    private readonly HttpClient _httpClient;
    private readonly ServiceRegistryEntryDto _serviceEntry;
    private readonly StringContent _serviceInfoContent;
    private Guid _serviceId;

    /// <summary>
    /// Gets a value indicating whether the service is connected or not.
    /// </summary>
    public bool IsConnected { get; private set; } = false;

    /// <summary>
    /// Initializes a new instance of <see cref="RegistryService"/>.
    /// </summary>
    /// <param name="logger">Logger.</param>
    /// <param name="configuration">App configuration.</param>
    /// <param name="httpClient">Http client.</param>
    public RegistryService(ILogger<RegistryService> logger, IConfiguration configuration, HttpClient httpClient)
    {
        _logger = logger;
        _httpClient = httpClient;
        var assembly = Assembly.GetEntryAssembly();
        var version = assembly?.GetName()?.Version;
        var versionString = "unknown";
        if (version != null)
        {
            versionString = version.ToString();
        }

        _serviceEntry = new ServiceRegistryEntryDto
        {
            Name = configuration.GetValue<string>("Atomy:Service:Name"),
            UniqueName = configuration.GetValue<string>("Atomy:Service:UniqueName"),
            Type = configuration.GetValue<string>("Atomy:Service:Type"),
            Url = configuration.GetValue<string>("Atomy:Service:Url"),
            Version = versionString
        };

        _serviceInfoContent = new StringContent(JsonSerializer.Serialize(_serviceEntry), Encoding.UTF8, "application/json");
        _httpClient.BaseAddress = new Uri(configuration.GetValue<string>("Atomy:ServiceRegistry:Url"));
    }

    /// <summary>
    /// Connects to the service registry.
    /// </summary>
    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Registry service is starting.");
        try
        {
            await Register(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogWarning("Failed to start", ex);
        }

        await base.StartAsync(cancellationToken);
    }

    /// <summary>
    /// Disconnects from the service registry.
    /// </summary>
    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Registry service is stopping.");
        try
        {
            var deleteResult = await _httpClient.DeleteAsync($"/Services?id={_serviceId}", cancellationToken);
            if (deleteResult.StatusCode != Sys.Net.HttpStatusCode.OK && deleteResult.StatusCode != Sys.Net.HttpStatusCode.NoContent)
            {
                _logger.LogWarning("Failed to unregister service with id {_serviceId} [Code: {deleteResult.StatusCode}]!", _serviceId, deleteResult.StatusCode);
            }

            IsConnected = false;
        }
        catch (Exception ex)
        {
            _logger.LogWarning("Failed to start", ex);
        }

        await base.StopAsync(cancellationToken);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return Task.Factory.StartNew(async () =>
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    if (IsConnected)
                    {
                        var putResult = await _httpClient.PutAsync("/Services", _serviceInfoContent, stoppingToken);
                        if (putResult.StatusCode != Sys.Net.HttpStatusCode.OK)
                        {
                            _logger.LogWarning("Failed to update service with id {_serviceId} [Code: {putResult.StatusCode}]!", _serviceId, putResult.StatusCode);
                            IsConnected = false;

                            // Try to register again. Because it is not costing much, we will try it infinite times.
                            await Register(stoppingToken);
                        }
                    }
                    else
                    {
                        await Register(stoppingToken);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogWarning("Failed to execute", ex);
                }

                await Task.Delay(TimeSpan.FromSeconds(30));
            }

        }, TaskCreationOptions.LongRunning);
    }

    private async Task Register(CancellationToken cancellationToken)
    {
        var postResult = await _httpClient.PostAsync("/Services", _serviceInfoContent, cancellationToken);

        if (postResult.StatusCode != Sys.Net.HttpStatusCode.OK)
        {
            _logger.LogWarning("Could not register {_serviceEntry.Name} [Code: {postResult.StatusCode}]!", _serviceEntry.Name, postResult.StatusCode);
        }
        else
        {
            using var stream = postResult.Content.ReadAsStream(cancellationToken);
            var streamReader = new StreamReader(stream, Encoding.UTF8);
            var providedId = streamReader.ReadToEnd().Trim(new char[] { '\\', '"' });
            _logger.LogInformation("Registry successful with ID: {providedId}.", providedId);
            _serviceId = Guid.Parse(providedId);
            IsConnected = true;
        }
    }
}