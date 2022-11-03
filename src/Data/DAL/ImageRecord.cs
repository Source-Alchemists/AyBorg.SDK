using Atomy.SDK.ImageProcessing;

namespace Atomy.SDK.DAL;

public record ImageRecord
{
    public int Width { get; set; }
    public int Height { get; set; }
    public int Size { get; set; }
    public PixelFormat PixelFormat { get; set; }
    public string Value { get; set; } = string.Empty;
}