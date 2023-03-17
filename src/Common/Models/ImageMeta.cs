using ImageTorque;

namespace AyBorg.SDK.Common.Models;

public readonly record struct ImageMeta
{
    public int Width { get; init; }
    public int Height { get; init; }
    public PixelFormat PixelFormat { get; init; }
}
