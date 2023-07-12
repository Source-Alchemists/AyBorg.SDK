using AyBorg.SDK.Common.Ports;

namespace AyBorg.SDK.Common;

public interface IStepBody : IPlugin
{
    /// <summary>
    /// Gets the ports.
    /// </summary>
    IEnumerable<IPort> Ports { get; }

    /// <summary>
    /// Tries to run asynchronous.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>True if successful.</returns>
    ValueTask<bool> TryRunAsync(CancellationToken cancellationToken);
}
