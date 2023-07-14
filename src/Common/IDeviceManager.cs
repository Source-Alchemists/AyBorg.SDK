namespace AyBorg.SDK.Common;

public interface IDeviceManager
{
    IReadOnlyCollection<IDevice> Devices { get; }

    IReadOnlyCollection<T> GetDevices<T>() where T : IDevice;

    T GetDevice<T>(string deviceId) where T : IDevice;
}
