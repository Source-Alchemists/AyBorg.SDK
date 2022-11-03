using Atomy.SDK.Common.Ports;

namespace Atomy.SDK.Common;

public interface IStepBody 
{
    /// <summary>
    /// Gets the default name.
    /// </summary>
    string DefaultName { get; }

    /// <summary>
    /// Gets the ports.
    /// </summary>
    IEnumerable<IPort> Ports { get; }

    /// <summary>
    /// Tries to run asynchronous.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    Task<bool> TryRunAsync(CancellationToken cancellationToken);
}