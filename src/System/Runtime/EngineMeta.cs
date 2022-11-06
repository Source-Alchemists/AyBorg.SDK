namespace Autodroid.SDK.System.Runtime;

public record EngineMeta
{
    /// <summary>
    /// Gets the identifier.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Gets or sets the state.
    /// </summary>
    public EngineState State { get; set; }

    /// <summary>
    /// Gets the execution type.
    /// </summary>
    public EngineExecutionType ExecutionType { get; init; }

    /// <summary>
    /// Gets the start date.
    /// </summary>
    public DateTime StartedAt { get; init; } = DateTime.UtcNow;

    /// <summary>
    /// Gets the end date.
    /// </summary>
    public DateTime StoppedAt { get; set; }
}