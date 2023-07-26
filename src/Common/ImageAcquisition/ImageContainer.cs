using ImageTorque;

namespace AyBorg.SDK.Common;

public sealed class ImageContainer : IDisposable
{
    private bool _isDisposed;
    public Image Image { get; }
    public long Index { get; }
    public string AdditionInfo { get; }

    public ImageContainer(Image image, long index, string additionalInfo)
    {
        Image = image;
        Index = index;
        AdditionInfo = additionalInfo;
    }

    private void Dispose(bool disposing)
    {
        if (!_isDisposed && disposing)
        {
            Image?.Dispose();
            _isDisposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
