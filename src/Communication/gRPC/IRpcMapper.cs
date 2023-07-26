using Ayborg.Gateway.Agent.V1;
using AyBorg.SDK.Common;
using AyBorg.SDK.Common.Models;
using AyBorg.SDK.Common.Ports;

namespace AyBorg.SDK.Communication.gRPC;

public interface IRpcMapper
{
    PluginMetaInfo FromRpc(PluginMetaDto rpc);
    Step FromRpc(StepDto rpc);
    Port FromRpc(PortDto rpc);
    Link FromRpc(LinkDto rpc);
    PluginMetaDto ToRpc(PluginMetaInfo pluginMetaInfo);
    StepDto ToRpc(Step step);
    PortDto ToRpc(Port port);
    LinkDto ToRpc(Link link);
    LinkDto ToRpc(PortLink link);
}
