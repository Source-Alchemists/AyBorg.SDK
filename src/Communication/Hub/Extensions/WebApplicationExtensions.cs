using AyBorg.Hub.Connect.Servers;
using Microsoft.AspNetCore.Builder;

namespace AyBorg.Hub.Connect;

public static class WebApplicationExtensions{
    public static WebApplication AddAyBorgAgentServer(this WebApplication app)
    {
        app.MapGrpcService<AgentServer>().RequireAuthorization();
        return app;
    }
}
