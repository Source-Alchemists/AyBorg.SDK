namespace AyBorg.SDK.Common;

public interface IDeviceManager
{
    /// <summary>
    /// Occurs when a device is changed.
    /// </summary>
    event EventHandler<ObjectChangedEventArgs> DeviceChanged;

    /// <summary>
    /// Occurs when a device is added or removed.
    /// </summary>
    event EventHandler<CollectionChangedEventArgs> DeviceCollectionChanged;

    /// <summary>
    /// Gets the devices.
    /// </summary>
    /// <returns>The devices.</returns>
    IEnumerable<T> GetDevices<T>() where T : IDevice;

    /// <summary>
    /// Gets the device.
    /// </summary>
    /// <param name="deviceId">The device identifier.</param>
    /// <returns>The device.</returns>
    T GetDevice<T>(string deviceId) where T : IDevice;
}
