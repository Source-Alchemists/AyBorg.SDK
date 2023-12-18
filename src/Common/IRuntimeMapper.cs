using AyBorg.SDK.Common.Models;
using AyBorg.SDK.Common.Ports;

namespace AyBorg.SDK.Common;

public interface IRuntimeMapper
{
    Step FromRuntime(IStepProxy stepProxy, bool skipPorts = false);
    Port FromRuntime(IPort runtimePort);
}
