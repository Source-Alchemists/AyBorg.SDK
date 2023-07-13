namespace AyBorg.SDK.Projects;

public interface IDeviceProviderProxy : IDisposable {
    string Name { get; }
    bool CanAdd { get; }
    IReadOnlyCollection<IDeviceProxy> Devices { get; }

    ValueTask<bool> TryInitializeAsync();
    ValueTask<IDeviceProxy> AddAsync(string id);
    ValueTask<IDeviceProxy> RemoveAsync(string id);
}
