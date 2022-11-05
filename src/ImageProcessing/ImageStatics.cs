using System.Runtime.CompilerServices;
using Atomy.SDK.ImageProcessing.Buffers;
using Atomy.SDK.ImageProcessing.Decoding;
using Atomy.SDK.ImageProcessing.Encoding;
using Atomy.SDK.ImageProcessing.Pixels;

namespace Atomy.SDK.ImageProcessing;

public partial record Image
{
    private static readonly PixelBufferConverter _pixelBufferConverter = new();
    private static readonly Decoder _decoder = new();
    private static readonly Encoder _encoder = new();

    /// <summary>
    /// Loads the image from the specified stream.
    /// </summary>
    /// <param name="stream">The stream.</param>
    /// <returns>The image.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Image Load(Stream stream)
    {
        var pixelBuffer = _decoder.Execute(new Decoding.Operations.DecoderParameters
        {
            Input = stream,
            OutputType = typeof(IPixelBuffer)
        });

        return new Image(pixelBuffer);
    }

    /// <summary>
    /// Loads the image from the specified file.
    /// </summary>
    /// <param name="path">The path.</param>
    /// <returns>The image.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Image Load(string path)
    {
        using var stream = File.OpenRead(path);
        return Load(stream);
    }

    /// <summary>
    /// Saves the image to the specified stream.
    /// </summary>
    /// <param name="image">The image.</param>
    /// <param name="stream">The stream.</param>
    /// <param name="encodeType">Type of the encode.</param>
    /// <param name="quality">The quality.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Save(Image image, Stream stream, EncoderType encodeType, int quality = 80)
    {
        IReadOnlyPixelBuffer pixelBuffer = image.PixelFormat switch
        {
            PixelFormat.Mono8 => image.AsPacked<Mono8>(),
            PixelFormat.Mono16 => image.AsPacked<Mono8>(),
            PixelFormat.Rgb24Packed => image.AsPacked<Rgb24>(),
            PixelFormat.Rgb48Packed => image.AsPacked<Rgb48>(),
            PixelFormat.Rgb888Planar => image.AsPacked<Rgb24>(),
            PixelFormat.Rgb161616Planar => image.AsPacked<Rgb48>(),
            PixelFormat.Mono => image.AsPacked<Mono8>(),
            PixelFormat.RgbPacked => image.AsPacked<Rgb24>(),
            _ => throw new NotSupportedException($"The pixel format {image.PixelFormat} is not supported."),
        };
        _encoder.Execute(new Encoding.Operations.EncoderParameters
        {
            Input = pixelBuffer,
            Stream = stream,
            EncoderType = encodeType,
            Quality = quality
        });
    }

    /// <summary>
    /// Saves the image as file in the specified format.
    /// </summary>
    /// <param name="image">The image.</param>
    /// <param name="path">The path.</param>
    /// <param name="encodeType">The encode type.</param>
    /// <param name="quality">The quality.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Save(Image image, string path, EncoderType encodeType, int quality = 80)
    {
        using var fileStream = File.OpenWrite(path);
        Save(image, fileStream, encodeType, quality);
    }

    /// <summary>
    /// Calculates the image clamp size. Keeping the aspect ratio.
    /// </summary>
    /// <param name="image">The image.</param>
    /// <param name="maxSize">The max size.</param>
    /// <param name="width">The width.</param>
    /// <param name="height">The height.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void CalculateClampSize(Image image, int maxSize, out int width, out int height)
    {
        if (image.Width > image.Height)
        {
            var per = (double)maxSize / image.Width;
            width = (int)(image.Width * per);
            height = (int)(image.Height * per);
        }
        else if (image.Height > image.Width)
        {
            var per = (double)maxSize / image.Height;
            width = (int)(image.Width * per);
            height = (int)(image.Height * per);
        }
        else
        {
            width = maxSize;
            height = maxSize;
        }
    }
}