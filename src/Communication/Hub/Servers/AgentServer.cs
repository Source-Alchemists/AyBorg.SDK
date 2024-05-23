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
        Microsoft.AspNetCore.Http.HttpContext httpContext = context.GetHttpContext();
        _logger.LogInformation((int)EventLogType.Connect, "Agent client with address {ClientAddress} connected.", httpContext.Connection.RemoteIpAddress?.ToString());
        return Task.FromResult(new Empty());
    }

    public override Task<Empty> Unregister(RegisterMessage request, ServerCallContext context)
    {
        Microsoft.AspNetCore.Http.HttpContext httpContext = context.GetHttpContext();
        _logger.LogInformation((int)EventLogType.Disconnect, "Agent client with address {ClientAddress} disconnected.", httpContext.Connection.RemoteIpAddress?.ToString());
        return Task.FromResult(new Empty());
    }
}
