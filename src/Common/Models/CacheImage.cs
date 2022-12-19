namespace AyBorg.SDK.Common.Models;

public sealed record CacheImage : ImageMeta, IDisposable
{
    private bool _disposedValue;
    public ImageProcessing.IImage? OriginalImage { get; set; }

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
