using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Autodroid.SDK.System.Configuration;

public record RegistryConfiguration : IRegistryConfiguration
{
    private readonly ILogger<IRegistryConfiguration> _logger;

    public string DisplayName { get; }

    public string UniqueName { get; }

    public string TypeName { get; }

    public string Version { get; }

    public string Url { get; }

    public RegistryConfiguration(ILogger<IRegistryConfiguration> logger, IConfiguration configuration)
    {
        _logger = logger;
        var assemblyName = Assembly.GetEntryAssembly()!.GetName();
        var serviceUniqueName = configuration.GetValue<string>("Autodroid:Service:UniqueName");
        if (string.IsNullOrEmpty(serviceUniqueName))
        {
            _logger.LogWarning("Service unique name is not set in configuration. Using default value. (Hint: Autodroid:Service:UniqueName)");
            UniqueName = assemblyName.Name!;
        }
        else
        {
            UniqueName = serviceUniqueName;
        }

        var serviceTypeName = configuration.GetValue<string>("Autodroid:Service:Type");
        if (string.IsNullOrEmpty(serviceTypeName))
        {
            _logger.LogWarning("Service type name is not set in configuration. Using default value. (Hint: Autodroid:Service:Type)");
            TypeName = assemblyName.Name!;
        }
        else
        {
            TypeName = serviceTypeName;
        }

        var serviceDisplayName = configuration.GetValue<string>("Autodroid:Service:DisplayName");
        if (string.IsNullOrEmpty(serviceDisplayName))
        {
            _logger.LogWarning("Service display name is not set in configuration. Using default value. (Hint: Autodroid:Service:DisplayName)");
            DisplayName = assemblyName.Name!;
        }
        else
        {
            DisplayName = serviceDisplayName;
        }

        var serviceUrl = configuration.GetValue<string>("Autodroid:Service:Url");
        if (string.IsNullOrEmpty(serviceUrl))
        {
            _logger.LogWarning("Service url is not set in configuration. Using default value. (Hint: Autodroid:Service:Url)");
            Url = "https://localhost:5001";
        }
        else
        {
            Url = serviceUrl;
        }

        Version = assemblyName.Version!.ToString();
    }
}