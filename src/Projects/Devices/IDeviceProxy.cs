using AyBorg.SDK.Common;

namespace AyBorg.SDK.Projects;

public interface IDeviceProxy
{
    string Id { get; }
    string Name { get; }
    bool IsActive { get; }
    bool IsConnected { get; }
    IReadOnlyCollection<string> Categories { get; }
    IDevice Native { get; }

    ValueTask<bool> TryConnectAsync();
    ValueTask<bool> TryDisconnectAsync();
}
