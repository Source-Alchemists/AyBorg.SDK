namespace AyBorg.SDK.Data.Bindings;

public sealed record Link
{
    public Guid Id { get; set; }
    public Guid SourceId { get; set; }
    public Guid TargetId { get; set; }
}
