namespace AyBorg.SDK.Common;

public interface IDeviceManager : IPlugin
{
    bool CanAdd { get; }

    bool CanRemove { get; }

    IReadOnlyCollection<IDevice> Devices { get; }

    ValueTask<IDevice> AddAsync(string id);

    ValueTask<IDevice> RemoveAsync(string id);
}
