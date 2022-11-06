using System.Collections.Concurrent;
using Autodroid.SDK.ImageProcessing.Buffers;
using Autodroid.SDK.ImageProcessing.Buffers.Operations;
using Autodroid.SDK.ImageProcessing.Pixels;

namespace Autodroid.SDK.ImageProcessing;

public partial record Image : IImage
{
    private readonly IPixelBuffer _rootPixelBuffer;
    private readonly ConcurrentDictionary<Type, IPixelBuffer> _convertedPixelBuffers = new();
    private bool _isDisposed = false;

    /// <summary>
    /// Gets the width.
    /// </summary>
    public int Width => _rootPixelBuffer.Width;

    /// <summary>
    /// Gets the height.
    /// </summary>
    public int Height => _rootPixelBuffer.Height;

    /// <summary>
    /// Gets the size.
    /// </summary>
    public int Size => _rootPixelBuffer.Size;

    public bool IsColor {
        get {
            return PixelFormat switch
            {
                PixelFormat.Mono or PixelFormat.Mono8 or PixelFormat.Mono16 => false,
                _ => true,
            };
        }
    }

    /// <summary>
    /// Gets the pixel format.
    /// </summary>
    public PixelFormat PixelFormat => _rootPixelBuffer.PixelFormat;

    /// <summary>
    /// Initializes a new instance of the <see cref="Image"/> class.
    /// </summary>
    /// <param name="pixelBuffer">The pixel buffer.</param>
    /// <remarks>The pixel buffer is owned by the image and should not be disposed.
    /// If you need to manipulate the image buffer outside of the image, you should create a copy.</remarks>
    public Image(IPixelBuffer pixelBuffer)
    {
        _rootPixelBuffer = pixelBuffer;
        _convertedPixelBuffers.TryAdd(pixelBuffer.GetType(), _rootPixelBuffer);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Image"/> class.
    /// </summary>
    /// <param name="other">The image to copy from.</param>
    public Image(Image other)
    {
        _rootPixelBuffer = PixelBufferMarshal.Copy(other._rootPixelBuffer);
        _convertedPixelBuffers = new ConcurrentDictionary<Type, IPixelBuffer>();
        _convertedPixelBuffers.TryAdd(_rootPixelBuffer.GetType(), _rootPixelBuffer);
    }

    /// <summary>
    /// Gets the image as a packed pixel buffer.
    /// </summary>
    /// <typeparam name="TPixel">The type of the pixel.</typeparam>
    /// <returns>The image as a packed pixel buffer.</returns>
    /// <remarks>The buffer is owned by the image and should not be disposed.</remarks>
    public ReadOnlyPackedPixelBuffer<TPixel> AsPacked<TPixel>()
        where TPixel : unmanaged, IPackedPixel<TPixel>
    {
        return (ReadOnlyPackedPixelBuffer<TPixel>)AsPixelBuffer<PackedPixelBuffer<TPixel>>();
    }

    /// <summary>
    /// Gets the image as a planar pixel buffer.
    /// </summary>
    /// <typeparam name="TPixel">The type of the pixel.</typeparam>
    /// <returns>The image as a planar pixel buffer.</returns>
    /// <remarks>The buffer is owned by the image and should not be disposed.</remarks>
    public ReadOnlyPlanarPixelBuffer<TPixel> AsPlanar<TPixel>()
        where TPixel : unmanaged, IPlanarPixel<TPixel>
    {
        return (ReadOnlyPlanarPixelBuffer<TPixel>)AsPixelBuffer<PlanarPixelBuffer<TPixel>>();
    }

    /// <summary>
    /// Indicates whether the two images are equal.
    /// </summary>
    /// <param name="other">The image to compare to.</param>
    /// <returns>True if the images are equal, false otherwise.</returns>
    public virtual bool Equals(Image? other)
    {
        if(other is null)
        {
            return false;
        }

        if(ReferenceEquals(this, other))
        {
            return true;
        }

        if(Width != other.Width || Height != other.Height || Size != other.Size || PixelFormat != other.PixelFormat)
        {
            return false;
        }

        return _rootPixelBuffer.Equals(other._rootPixelBuffer);
    }

    /// <summary>
    /// Gets a hash code for this instance.
    /// </summary>
    /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
    public override int GetHashCode()
    {
        return HashCode.Combine(_rootPixelBuffer);
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_isDisposed)
        {
            if (disposing)
            {
                _rootPixelBuffer.Dispose();
                foreach (var pixelBuffer in _convertedPixelBuffers.Values)
                {
                    pixelBuffer.Dispose();
                }
            }

            _isDisposed = true;
        }
    }

    private IReadOnlyPixelBuffer AsPixelBuffer<TBuffer>()
    {
        if (_convertedPixelBuffers.TryGetValue(typeof(TBuffer), out var pixelBuffer))
        {
            return pixelBuffer.AsReadOnly();
        }

        var convertedPixelBuffer = _pixelBufferConverter.Execute(new ConvertParameters
        {
            Input = _rootPixelBuffer.AsReadOnly(),
            OutputType = typeof(TBuffer)
        });

        _convertedPixelBuffers.TryAdd(typeof(TBuffer), convertedPixelBuffer);
        return convertedPixelBuffer.AsReadOnly();
    }
}