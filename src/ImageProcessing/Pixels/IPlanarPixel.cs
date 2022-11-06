namespace Autodroid.SDK.ImageProcessing.Pixels;

public interface IPlanarPixel<T> : IPixel<T>
    where T : unmanaged, IPixel<T>
{
}