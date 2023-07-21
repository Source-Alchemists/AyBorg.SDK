using AyBorg.SDK.Common;
using AyBorg.SDK.Common.Ports;

namespace AyBorg.SDK.Projects;

public interface IDeviceProxy
{
    /// <summary>
    /// Gets the id.
    /// </summary>
    string Id { get; }

    /// <summary>
    /// Gets the name.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Gets the manufacturer.
    /// </summary>
    string Manufacturer { get; }

    /// <summary>
    /// Gets a value indicating whether is active.
    /// </summary>
    bool IsActive { get; }

    /// <summary>
    /// Gets a value indicating whether is connected.
    /// </summary>
    bool IsConnected { get; }

    /// <summary>
    /// Gets the categories.
    /// </summary>
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

    /// <summary>
    /// Gets the native device.
    /// </summary>
    IDevice Native { get; }

    /// <summary>
    /// Try to connect to the device.
    /// </summary>
    /// <returns>True if successful.</returns>
    ValueTask<bool> TryConnectAsync();

    /// <summary>
    /// Try to disconnect from the device.
    /// </summary>
    /// <returns>True if successful.</returns>
    ValueTask<bool> TryDisconnectAsync();
}
