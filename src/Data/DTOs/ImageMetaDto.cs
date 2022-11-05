using Atomy.SDK.ImageProcessing;
using Atomy.SDK.ImageProcessing.Encoding;

namespace Atomy.SDK.Data.DTOs;

public sealed record ImageMetaDto
{
    public int Width { get; set; }
    public int Height { get; set; }
    public PixelFormat PixelFormat { get; set; }
    public EncoderType EncoderType { get; set; }
}