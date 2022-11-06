using Autodroid.SDK.ImageProcessing.Pixels;

namespace Autodroid.SDK.ImageProcessing.Buffers;

public abstract record ReadOnlyPixelBuffer<TPixel> : IReadOnlyPixelBuffer<TPixel>
    where TPixel : unmanaged, IPixel<TPixel>
{
    /// <summary>
    /// Gets the pixel buffer.
    /// </summary>
    protected IPixelBuffer<TPixel> PixelBuffer { get; }

    /// <summary>
    /// Gets the pixels.
    /// </summary>
    public ReadOnlySpan<TPixel> Pixels { get => PixelBuffer.Pixels; }

    /// <summary>
    /// Gets the buffer.
    /// </summary>
    public ReadOnlySpan<byte> Buffer { get => PixelBuffer.Buffer; }

    /// <summary>
    /// Gets the width.
    /// </summary>
    public int Width { get => PixelBuffer.Width; }

    /// <summary>
    /// Gets the height.
    /// </summary>
    public int Height { get => PixelBuffer.Height; }

    /// <summary>
    /// Gets the number of channels.
    /// </summary>
    public int NumberOfChannels { get => PixelBuffer.NumberOfChannels; }

    /// <summary>
    /// Gets the size.
    /// </summary>
    public int Size { get => PixelBuffer.Size; }
    
    /// <summary>
    /// Gets the pixel buffer type.
    /// </summary>
    public PixelBufferType PixelBufferType { get => PixelBuffer.PixelBufferType; }

    /// <summary>
    /// Gets the pixel info.
    /// </summary>
    public PixelInfo PixelInfo { get => PixelBuffer.PixelInfo; }

    /// <summary>
    /// Gets the pixel format.
    /// </summary>
    public PixelFormat PixelFormat { get => PixelBuffer.PixelFormat; }

    /// <summary>
    /// Gets the pixel at the specified index.
    /// </summary>
    public TPixel this[int index] { get => PixelBuffer[index]; }

    /// <summary>
    /// Gets the pixel at the specified index.
    /// </summary>
    public TPixel this[int x, int y] { get => PixelBuffer[x, y]; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ReadOnlyPixelBuffer{TPixel}"/> class.
    /// </summary>
    /// <param name="pixelBuffer">The pixel buffer.</param>
    protected ReadOnlyPixelBuffer(IPixelBuffer<TPixel> pixelBuffer)
    {
        PixelBuffer = pixelBuffer;
    } 

    /// <summary>
    /// Gets the channel.
    /// </summary>
    public abstract ReadOnlySpan<TPixel> GetChannel(int channelIndex);
}