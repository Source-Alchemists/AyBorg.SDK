namespace Atomy.SDK.Common;

public abstract record BaseLink<T> {

    /// <summary>
    /// Gets the identifier.
    /// </summary>
    public Guid Id { get; protected set; }
    
    /// <summary>
    /// Gets the source identifier.
    /// </summary>
    public Guid SourceId { get; protected set; }

    /// <summary>
    /// Gets the target identifier.
    /// </summary>
    public Guid TargetId { get; protected set; }
}