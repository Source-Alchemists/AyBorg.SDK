namespace AyBorg.SDK.System.Caching;

public record StepCacheKey
{
    /// <summary>
    /// Gets the iteration identifier.
    /// </summary>
    public Guid IterationId { get; init; }

    /// <summary>
    /// Gets the step identifier.
    /// </summary>
    public Guid StepId { get; init; }
}
