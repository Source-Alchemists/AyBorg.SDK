namespace AyBorg.SDK.Data.DTOs;

public sealed record LinkDto {

    /// <summary>
    /// Gets the identifier.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the source identifier.
    /// </summary>
    public Guid SourceId { get; set; }
    
    /// <summary>
    /// Gets or sets the target identifier.
    /// </summary>
    public Guid TargetId { get; set; }
}