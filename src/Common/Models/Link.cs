namespace AyBorg.SDK.Common.Models;

public sealed record Link
{
    public Guid Id { get; set; }
    public Guid SourceId { get; set; }
    public Guid TargetId { get; set; }
}
