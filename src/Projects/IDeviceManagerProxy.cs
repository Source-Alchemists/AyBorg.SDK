namespace AyBorg.SDK.Projects;

public interface IDeviceManagerProxy : IDisposable {
    bool CanAdd { get; }
    bool CanRemove { get; }

    ValueTask<bool> TryInitializeAsync();
    ValueTask<IDeviceProxy> AddAsync(string id);
    ValueTask RemoveAsync(string id);
}
