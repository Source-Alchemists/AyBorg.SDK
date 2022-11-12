namespace AyBorg.SDK.ImageProcessing.Pixels;

public interface IPackedPixel<T> : IPixel<T> where T : unmanaged, IPixel<T>
{
}