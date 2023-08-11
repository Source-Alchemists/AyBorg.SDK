using AyBorg.SDK.Common;
using AyBorg.SDK.Common.Ports;

namespace AyBorg.SDK.Common;

public interface IStepProxy : IDisposable
{
    /// <summary>
    /// Called when the step is executed.
    /// </summary>
    event EventHandler<bool> Completed;

    /// <summary>
    /// Gets the identifier.
    /// </summary>
    /// <value>
    /// The identifier.
    /// </value>
    Guid Id { get; }

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    string Name { get; set; }

    /// <summary>
    /// Gets the categories.
    /// </summary>
    IReadOnlyCollection<string> Categories { get; }

    /// <summary>
    /// Gets or sets the meta information.
    /// </summary>
    /// <value>
    /// The meta information.
    /// </value>
    PluginMetaInfo MetaInfo { get; }

    /// <summary>
    /// Gets the ports.
    /// </summary>
    /// <value>
    /// The ports.
    /// </value>
    IEnumerable<IPort> Ports { get; }

    /// <summary>
    /// Gets the links.
    /// </summary>
    IList<PortLink> Links { get; }

    /// <summary>
    /// Gets the step body.
    /// </summary>
    /// <value>
    /// The step body.
    /// </value>
    IStepBody StepBody { get; }

    /// <summary>
    /// Gets or sets the x.
    /// </summary>
    /// <value>
    /// The x.
    /// </value>
    int X { get; set; }

    /// <summary>
    /// Gets or sets the y.
    /// </summary>
    /// <value>
    /// The y.
    /// </value>
    int Y { get; set; }

    /// <summary>
    /// Gets the iteration identifier the step was execution last in.
    /// </summary>
    Guid IterationId { get; }

    /// <summary>
    /// Gets the execution time in milliseconds.
    /// </summary>
    long ExecutionTimeMs { get; }

    /// <summary>
    /// Executes the step.
    /// </summary>
    /// <param name="iterationId">The iteration identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    ValueTask<bool> TryRunAsync(Guid iterationId, CancellationToken cancellationToken);

    /// <summary>
    /// Initializes the step before running it.
    /// </summary>
    ValueTask<bool> TryBeforeStartAsync();

    /// <summary>
    /// Called after the step is created or loaded.
    /// </summary>
    ValueTask<bool> TryAfterInitializedAsync();
}
