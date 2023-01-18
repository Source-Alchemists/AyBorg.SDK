using Ayborg.Gateway.Agent.V1;
using AyBorg.SDK.Common;
using AyBorg.SDK.Common.Models;
using AyBorg.SDK.Common.Ports;

namespace AyBorg.SDK.Communication.gRPC;

public interface IRpcMapper
{
    Step FromRpc(StepDto rpc);
    StepDto ToRpc(Step step);
    PluginMetaInfo FromRpc(PluginMetaDto rpc);
    PluginMetaDto ToRpc(PluginMetaInfo pluginMetaInfo);
    Port FromRpc(PortDto rpc);
    PortDto ToRpc(Port port);
    LinkDto ToRpc(Link link);
    LinkDto ToRpc(PortLink link);
    Link FromRpc(LinkDto rpc);
}
