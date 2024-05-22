using AyBorg.SDK.Common;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace AyBorg.Hub.Connect.Servers;

public sealed class AgentServer : AgentConnection.AgentConnectionBase
{
    private readonly ILogger<AgentServer> _logger;

    public AgentServer(ILogger<AgentServer> logger)
    {
        _logger = logger;
    }
    public override Task<Empty> Register(RegisterMessage request, ServerCallContext context)
    {
        _logger.LogInformation((int)EventLogType.Connect, "Agent client with host address {HostAddress} connected.", context.Host);
        return Task.FromResult(new Empty());
    }
}
