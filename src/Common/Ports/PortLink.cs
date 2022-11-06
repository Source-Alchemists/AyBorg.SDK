namespace Autodroid.SDK.Common.Ports;

public sealed record PortLink : BaseLink<IPort>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PortLink"/> class.
    /// </summary>
    /// <param name="source">The source.</param>
    /// <param name="target">The target.</param>
    public PortLink(IPort source, IPort target)
    {
        Id = Guid.NewGuid();
        SourceId = source.Id;
        TargetId = target.Id;

        Source = source;
        Target = target;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PortLink"/> class.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="source">The source port.</param>
    /// <param name="target">The target port.</param>
    public PortLink(Guid id, IPort source, IPort target)
    {
        Id = id;
        SourceId = source.Id;
        TargetId = target.Id;

        Source = source;
        Target = target;
    }

    /// <summary>
    /// Gets the source.
    /// </summary>
    public IPort Source { get; }

    /// <summary>
    /// Gets the target.
    /// </summary>
    public IPort Target { get; }

    /// <summary>
    /// Gets the source.
    /// </summary>
    /// <returns>Source port as specific type.</returns>
    public TPort GetSource<TPort>() where TPort : IPort
    {
        return (TPort)Source;
    }
}