using AyBorg.SDK.ImageProcessing;

namespace AyBorg.SDK.Data.Bindings;

public sealed record ImageMeta
{
    public int Width { get; set; }
    public int Height { get; set; }
    public PixelFormat PixelFormat { get; set; }
}
