using AyBorg.Hub.Connect.Models;
using Google.Protobuf.WellKnownTypes;

namespace AyBorg.Hub.Connect.Clients;

public sealed class AgentClient
{
    private readonly AgentConnection.AgentConnectionClient _client;

    public AgentClient(AgentConnection.AgentConnectionClient client)
    {
        _client = client;
    }

    public async ValueTask RegisterAsync(RegisterParameters parameters)
    {
        await _client.RegisterAsync(new RegisterMessage
        {
            ServiceId = parameters.ServiceId,
            Name = parameters.Name,
            Type = parameters.Type,
            Version = parameters.Version
        }).ConfigureAwait(false);
    }

    public async ValueTask UnregisterAsync(RegisterParameters parameters)
    {
        await _client.UnregisterAsync(new RegisterMessage
        {
            ServiceId = parameters.ServiceId,
            Name = parameters.Name,
            Type = parameters.Type,
            Version = parameters.Version
        }).ConfigureAwait(false);
    }

    public async ValueTask NotifyAsync(NotifyParameters parameters)
    {
        await _client.NotifyAsync(new NotifyMessage
        {
            ServiceId = parameters.ServiceId,
            Type = (int)parameters.NotifyType,
            Payload = parameters.Payload
        }).ConfigureAwait(false);
    }
}
