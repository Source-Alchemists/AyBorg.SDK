using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AyBorg.SDK.System.Configuration;

public record ServiceConfiguration : RegistryConfiguration, IServiceConfiguration
{
    private readonly ILogger<IServiceConfiguration> _logger;

    public string RegistryUrl { get; }

    public ServiceConfiguration(ILogger<IServiceConfiguration> logger, IConfiguration configuration) : base(logger, configuration)
    {
        _logger = logger;
        var registryUrl = configuration.GetValue<string>("AyBorg:Registry:Url");
        if (string.IsNullOrEmpty(registryUrl))
        {
            _logger.LogWarning("Registry url is not set in configuration. Using default value. (Hint: AyBorg:Registry:Url)");
            RegistryUrl = "https://localhost:5001";
        }
        else
        {
            RegistryUrl = registryUrl;
        }
    }
}