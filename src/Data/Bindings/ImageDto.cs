using AyBorg.SDK.Data.Bindings;

namespace AyBorg.SDK.Data.DTOs;

public sealed record ImageDto
{
    public ImageMeta Meta { get; set; } = null!;
    public string Base64 { get; set; } = string.Empty;
}
