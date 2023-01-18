using ImageTorque;

namespace AyBorg.SDK.Common.Models;

public record ImageMeta
{
    public int Width { get; set; }
    public int Height { get; set; }
    public PixelFormat PixelFormat { get; set; }
}
