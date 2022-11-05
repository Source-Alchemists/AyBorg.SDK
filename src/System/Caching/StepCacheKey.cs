namespace Atomy.SDK.System.Caching;

public record StepCacheKey
{
    /// <summary>
    /// Gets the iteration identifier.
    /// </summary>
    public Guid IterationId { get; }

    /// <summary>
    /// Gets the step identifier.
    /// </summary>
    public Guid StepId { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="StepCacheKey"/> class.
    /// </summary>
    /// <param name="iterationId">The iteration identifier.</param>
    /// <param name="stepId">The step identifier.</param>
    public StepCacheKey(Guid iterationId, Guid stepId)
    {
        IterationId = iterationId;
        StepId = stepId;
    }
}