using AyBorg.SDK.Common.Ports;

namespace AyBorg.SDK.Common;

public interface IDevice : IPlugin
{
    string Id { get; }

    bool IsConnected { get; }

    /// <summary>
    /// Gets the ports.
    /// </summary>
    IEnumerable<IPort> Ports { get; }

    ValueTask<bool> TryConnectAsync();
    ValueTask<bool> TryDisconnectAsync();
}
