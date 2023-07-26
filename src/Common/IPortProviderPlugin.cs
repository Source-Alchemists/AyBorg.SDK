using AyBorg.SDK.Common.Ports;

namespace AyBorg.SDK.Common;

public interface IPortProviderPlugin : IPlugin {
    /// <summary>
    /// Gets the ports.
    /// </summary>
    IReadOnlyCollection<IPort> Ports { get; }
}
