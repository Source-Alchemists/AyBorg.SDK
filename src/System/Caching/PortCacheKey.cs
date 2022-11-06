namespace Autodroid.SDK.System.Caching;

public record PortCacheKey
{
    /// <summary>
    /// Gets the iteration identifier.
    /// </summary>
    public Guid IterationId { get; }

    /// <summary>
    /// Gets the port identifier.
    /// </summary>
    public Guid PortId { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="PortCacheKey"/> class.
    /// </summary>
    /// <param name="iterationId">The iteration identifier.</param>
    /// <param name="portId">The port identifier.</param>
    public PortCacheKey(Guid iterationId, Guid portId)
    {
        IterationId = iterationId;
        PortId = portId;
    }
}