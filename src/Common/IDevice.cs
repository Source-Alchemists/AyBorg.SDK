using AyBorg.SDK.Common.Ports;

namespace AyBorg.SDK.Common;

public interface IDevice : IPortProviderPlugin
{
    string Id { get; }

    bool IsConnected { get; }

    ValueTask<bool> TryConnectAsync();
    ValueTask<bool> TryDisconnectAsync();
    ValueTask<bool> TryUpdate(IReadOnlyCollection<IPort> ports);
}
