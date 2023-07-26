namespace AyBorg.SDK.Common;

public interface IDeviceProvider : IPlugin
{
    /// <summary>
    /// Gets the device provider prefix.
    /// </summary>
    string Prefix { get; }

    /// <summary>
    /// Gets a value indicating whether can create.
    /// </summary>
    bool CanCreate { get; }

    /// <summary>
    /// Creates a new device.
    /// </summary>
    /// <param name="id">The id.</param>
    /// <returns>The device.</returns>
    ValueTask<IDevice> CreateAsync(string id);
}
