using Microsoft.Extensions.Logging;

namespace AyBorg.SDK.Communication.MQTT;

/// <summary>
/// Factory for creating MQTT client provider.
/// </summary>
public interface IMqttClientProviderFactory
{
    /// <summary>
    /// Creates the MQTT client provider.
    /// </summary>
    /// <param name="logger">The logger.</param>
    /// <param name="clientId">The client identifier.</param>
    /// <param name="host">The host.</param>
    /// <param name="port">The port.</param>
    /// <returns>New mqtt client provider.</returns>
    IMqttClientProvider Create(ILogger logger, string clientId, string host, int port);
}
