using Atomy.SDK.ImageProcessing;
using Atomy.SDK.ImageProcessing.Encoding;

namespace Atomy.SDK.DTOs;

public record ImageMetaDto
{
    public int Width { get; set; }
    public int Height { get; set; }
    public PixelFormat PixelFormat { get; set; }
    public EncoderType EncoderType { get; set; }
}