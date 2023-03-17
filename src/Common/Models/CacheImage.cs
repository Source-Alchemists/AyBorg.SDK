namespace AyBorg.SDK.Common.Models;

public sealed record CacheImage : IDisposable
{
    private bool _disposedValue;
    public ImageTorque.IImage? OriginalImage { get; init; }

    public ImageMeta Meta { get; init; }

    private void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                OriginalImage?.Dispose();
            }
            _disposedValue = true;
        }
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
