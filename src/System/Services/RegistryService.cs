using Sys = System;
using System.Text;
using System.Text.Json;
using AyBorg.SDK.Data.DTOs;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using AyBorg.SDK.System.Configuration;

namespace AyBorg.SDK.System.Services;

public class RegistryService : BackgroundService
{
    private readonly ILogger<RegistryService> _logger;
    private readonly HttpClient _httpClient;
    private readonly RegistryEntryDto _serviceEntry;
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
    /// <param name="serviceConfiguration">App configuration.</param>
    /// <param name="httpClient">Http client.</param>
    public RegistryService(ILogger<RegistryService> logger, IServiceConfiguration serviceConfiguration, HttpClient httpClient)
    {
        _logger = logger;
        _httpClient = httpClient;
        _serviceEntry = new RegistryEntryDto
        {
            Name = serviceConfiguration.DisplayName,
            UniqueName = serviceConfiguration.UniqueName,
            Type = serviceConfiguration.TypeName,
            Url = serviceConfiguration.Url,
            Version = serviceConfiguration.Version
        };

        _serviceInfoContent = new StringContent(JsonSerializer.Serialize(_serviceEntry), Encoding.UTF8, "application/json");
        _httpClient.BaseAddress = new Uri(serviceConfiguration.RegistryUrl);
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
            HttpResponseMessage deleteResult = await _httpClient.DeleteAsync($"/Services?id={_serviceId}", cancellationToken);
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

    protected override Task ExecuteAsync(CancellationToken stoppingToken) => Task.Factory.StartNew(async () =>
                                                                                  {
                                                                                      while (!stoppingToken.IsCancellationRequested)
                                                                                      {
                                                                                          try
                                                                                          {
                                                                                              if (IsConnected)
                                                                                              {
                                                                                                  HttpResponseMessage putResult = await _httpClient.PutAsync("/Services", _serviceInfoContent, stoppingToken);
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

    private async ValueTask Register(CancellationToken cancellationToken)
    {
        HttpResponseMessage postResult = await _httpClient.PostAsync("/Services", _serviceInfoContent, cancellationToken);

        if (postResult.StatusCode != Sys.Net.HttpStatusCode.OK)
        {
            _logger.LogWarning("Could not register {_serviceEntry.Name} [Code: {postResult.StatusCode}]!", _serviceEntry.Name, postResult.StatusCode);
        }
        else
        {
            using Stream stream = postResult.Content.ReadAsStream(cancellationToken);
            var streamReader = new StreamReader(stream, Encoding.UTF8);
            string providedId = streamReader.ReadToEnd().Trim(new char[] { '\\', '"' });
            _logger.LogInformation("Registry successful with ID: {providedId}.", providedId);
            _serviceId = Guid.Parse(providedId);
            IsConnected = true;
        }
    }
}
