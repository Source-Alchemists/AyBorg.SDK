using AyBorg.SDK.Common.Ports;

namespace AyBorg.SDK.Common;

public interface IDevice : IPlugin {
    /// <summary>
    /// Gets the ports.
    /// </summary>
    IEnumerable<IPort> Ports { get; }

    ValueTask<bool> TryConnectAsync();
}
