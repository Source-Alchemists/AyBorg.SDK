using AyBorg.SDK.Common.Ports;

namespace AyBorg.SDK.Common;

public interface IStepBody
{
    /// <summary>
    /// Gets the default name.
    /// </summary>
    string DefaultName { get; }

    /// <summary>
    /// Gets the categories.
    /// </summary>
    IReadOnlyCollection<string> Categories { get; }

    /// <summary>
    /// Gets the ports.
    /// </summary>
    IEnumerable<IPort> Ports { get; }

    /// <summary>
    /// Tries to run asynchronous.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    ValueTask<bool> TryRunAsync(CancellationToken cancellationToken);
}
