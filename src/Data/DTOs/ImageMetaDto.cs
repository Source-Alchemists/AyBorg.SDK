using Autodroid.SDK.ImageProcessing;
using Autodroid.SDK.ImageProcessing.Encoding;

namespace Autodroid.SDK.Data.DTOs;

public sealed record ImageMetaDto
{
    public int Width { get; set; }
    public int Height { get; set; }
    public PixelFormat PixelFormat { get; set; }
    public EncoderType EncoderType { get; set; }
}