namespace AyBorg.SDK.Common;

public interface IDeviceManager
{
    IEnumerable<IDevice> Devices { get; }

    IEnumerable<T> GetDevices<T>() where T : IDevice;

    T GetDevice<T>(string deviceId) where T : IDevice;
}
