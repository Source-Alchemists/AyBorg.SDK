namespace AyBorg.SDK.Common;

public interface IDeviceProvider : IPlugin
{
    bool CanCreate { get; }

    ValueTask<IDevice> CreateAsync(string id);
}
