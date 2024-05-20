using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AyBorg.SDK.System.Configuration;

public record ServiceConfiguration : GatewayConfiguration, IServiceConfiguration
{
    private readonly ILogger<ServiceConfiguration> _logger;

    public string GatewayUrl { get; }

    public ServiceConfiguration(ILogger<ServiceConfiguration> logger, IConfiguration configuration) : base(logger, configuration)
    {
        _logger = logger;
        string? registryUrl = configuration.GetValue<string>("AyBorg:Gateway:Url");
        if (string.IsNullOrEmpty(registryUrl))
        {
            if (configuration.GetValue<string>("AyBorg:Service:Type")?.Equals(ServiceTypes.Gateway) != true)
            {
                _logger.LogWarning("Registry url is not set in configuration. Using default value. (Hint: AyBorg:Gateway:Url)");
            }
            GatewayUrl = "http://localhost:5000";
        }
        else
        {
            GatewayUrl = registryUrl;
        }
    }
}
