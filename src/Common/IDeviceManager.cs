namespace AyBorg.SDK.Common;

public interface IDeviceManager
{
    event EventHandler<ObjectChangedEventArgs> DeviceChanged;
    event EventHandler<CollectionChangedEventArgs> DeviceCollectionChanged;

    IEnumerable<IDevice> Devices { get; }

    IEnumerable<T> GetDevices<T>() where T : IDevice;

    T GetDevice<T>(string deviceId) where T : IDevice;
}
