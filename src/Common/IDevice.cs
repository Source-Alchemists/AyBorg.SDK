using AyBorg.SDK.Common.Ports;

namespace AyBorg.SDK.Common;

public interface IDevice : IPortProviderPlugin
{
    /// <summary>
    // Gets the id.
    /// </summary>
    string Id { get; }

    /// <summary>
    /// Gets the manufacturer.
    /// </summary>
    string Manufacturer { get; }

    /// <summary>
    /// Gets a value indicating whether is connected.
    /// </summary>
    bool IsConnected { get; }

    /// <summary>
    /// Try to connect.
    /// </summary>
    /// <returns>True if connected.</returns>
    ValueTask<bool> TryConnectAsync();

    /// <summary>
    /// Try to disconnect.
    /// </summary>
    /// <returns>True if disconnected.</returns>
    ValueTask<bool> TryDisconnectAsync();

    /// <summary>
    /// Try to update.
    /// </summary>
    /// <param name="ports">The ports.</param>
    /// <returns>True if updated.</returns>
    ValueTask<bool> TryUpdateAsync(IReadOnlyCollection<IPort> ports);
}
