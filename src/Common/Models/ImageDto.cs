namespace AyBorg.SDK.Common.Models;

public sealed record ImageDto
{
    public ImageMeta Meta { get; set; } = null!;
    public string Base64 { get; set; } = string.Empty;
}
