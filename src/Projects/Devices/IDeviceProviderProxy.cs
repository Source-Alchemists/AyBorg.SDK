using AyBorg.SDK.Common;

namespace AyBorg.SDK.Projects;

public interface IDeviceProviderProxy : IDisposable {

    /// <summary>
    /// Gets the device provider name.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Gets the device provider prefix.
    /// </summary>
    string Prefix { get; }

    /// <summary>
    /// Gets a value indicating whether can add.
    /// </summary>
    bool CanAdd { get; }

    /// <summary>
    /// Gets the plugin meta information.
    /// </summary>
    PluginMetaInfo MetaInfo { get; }

    /// <summary>
    /// Gets the devices.
    /// </summary>
    IReadOnlyCollection<IDeviceProxy> Devices { get; }

    /// <summary>
    /// Try to initialize.
    /// </summary>
    /// <returns>True if initialized.</returns>
    ValueTask<bool> TryInitializeAsync();

    /// <summary>
    /// Try to add.
    /// </summary>
    /// <param name="options">The options.</param>
    /// <returns>The device.</returns>
    ValueTask<IDeviceProxy> AddAsync(AddDeviceOptions options);

    /// <summary>
    /// Try to remove.
    /// </summary>
    /// <param name="id">The id.</param>
    /// <returns>The device.</returns>
    ValueTask<IDeviceProxy> RemoveAsync(string id);
}
