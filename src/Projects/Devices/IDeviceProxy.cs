using AyBorg.SDK.Common;
using AyBorg.SDK.Common.Ports;

namespace AyBorg.SDK.Projects;

public interface IDeviceProxy
{
    string Id { get; }
    string Name { get; }
    bool IsActive { get; }
    bool IsConnected { get; }
    IReadOnlyCollection<string> Categories { get; }

    /// <summary>
    /// Gets the ports.
    /// </summary>
    IEnumerable<IPort> Ports { get; }

    /// <summary>
    /// Gets or sets the meta information.
    /// </summary>
    PluginMetaInfo MetaInfo { get; }

    /// <summary>
    /// Gets or sets the provider meta information.
    /// </summary>
    PluginMetaInfo ProviderMetaInfo { get; }

    IDevice Native { get; }

    ValueTask<bool> TryConnectAsync();
    ValueTask<bool> TryDisconnectAsync();
}
