namespace AyBorg.SDK.Common;

public interface IDeviceManager : IPlugin
{
    bool CanCreate { get; }

    ValueTask<IDevice> CreateAsync(string id);
}
