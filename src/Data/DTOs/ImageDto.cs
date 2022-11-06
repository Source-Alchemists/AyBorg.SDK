namespace Autodroid.SDK.Data.DTOs;

public sealed record ImageDto
{
    public ImageMetaDto Meta { get; set; } = null!;
    public string Base64 { get; set; } = string.Empty;
}