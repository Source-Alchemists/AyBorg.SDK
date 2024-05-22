using AyBorg.Hub.Connect.Clients;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AyBorg.Hub.Connect;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddAyBorgAgentClient(this WebApplicationBuilder builder)
    {
        IConfigurationSection hubClientOptionsSection = builder.Configuration.GetSection(HubClientOptions.AyBorgHubClient);
        var hubClientOptions = new HubClientOptions();
        hubClientOptionsSection.Bind(hubClientOptions);
        builder.Services.Configure<HubClientOptions>(hubClientOptionsSection);

        builder.Services.AddGrpcClient<AgentConnection.AgentConnectionClient>(factoryOptions =>
        {
            factoryOptions.ChannelOptionsActions.Add(o => o.UnsafeUseInsecureChannelCallCredentials = hubClientOptions.AllowInsecureChannel);
            factoryOptions.Address = hubClientOptions.HubAddress;
        });

        builder.Services.AddHostedService<AgentRegisterBackgroundService>();

        builder.Services.AddSingleton<AgentClient>();
        return builder;
    }
}
