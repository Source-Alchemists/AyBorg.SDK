namespace Atomy.SDK.Data.DTOs;

public record ImageDto
{
    public ImageMetaDto Meta { get; set; } = null!;
    public string Base64 { get; set; } = string.Empty;
}