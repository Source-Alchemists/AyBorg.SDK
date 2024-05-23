using AyBorg.Hub.Connect.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AyBorg.Hub.Connect.Clients;

public sealed class AgentRegisterBackgroundService : RegisterBackgroundService<AgentConnection.AgentConnectionClient>
{
    public AgentRegisterBackgroundService(ILogger<AgentRegisterBackgroundService> logger, AgentConnection.AgentConnectionClient client, IOptions<HubClientOptions> options) : base(logger, client, options)
    {

    }

    protected override async ValueTask RegisterAsync(CancellationToken cancellationToken)
    {
        await _client.RegisterAsync(new RegisterMessage
        {
            ServiceId = _options.Value.ServiceUniqueName,
            Name = _options.Value.ServiceName,
            Type = _options.Value.ServiceType,
            Version = _serviceVersion
        }, cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    protected override async ValueTask UnregisterAsync(CancellationToken cancellationToken)
    {
        await _client.UnregisterAsync(new RegisterMessage
        {
            ServiceId = _options.Value.ServiceUniqueName,
            Name = _options.Value.ServiceName,
            Type = _options.Value.ServiceType,
            Version = _serviceVersion
        }, cancellationToken: cancellationToken).ConfigureAwait(false);
    }
}
