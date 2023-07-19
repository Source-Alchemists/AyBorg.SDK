using AyBorg.SDK.Common;

namespace AyBorg.SDK.Projects;

public interface IDeviceProviderProxy : IDisposable {
    string Name { get; }
    bool CanAdd { get; }
    PluginMetaInfo MetaInfo { get; }
    IReadOnlyCollection<IDeviceProxy> Devices { get; }

    ValueTask<bool> TryInitializeAsync();
    ValueTask<IDeviceProxy> AddAsync(AddDeviceOptions options);
    ValueTask<IDeviceProxy> RemoveAsync(string id);
}
