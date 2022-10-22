using System.Runtime.InteropServices;
using Atomy.SDK.ImageProcessing.Buffers;
using Atomy.SDK.ImageProcessing.Pixels;
using SixLabors.ImageSharp;

namespace Atomy.SDK.ImageProcessing.Decoding.Operations;

public class DecoderOperations : Decoder
{
    private static Configuration _configuration = Configuration.Default;

    public DecoderOperations()
    {
        _configuration = Configuration.Default;
        _configuration.PreferContiguousImageBuffers = true;
        AddOperation<Stream, IPixelBuffer>(DecodeStream);
    }

    private static IPixelBuffer DecodeStream(DecoderParameters parameters)
    {
        var stream = parameters.Input;
        if (stream == null)
            throw new ArgumentNullException(nameof(parameters.Input));

        long position = stream.Position;
        var info = SixLabors.ImageSharp.Image.Identify(stream);
        if (info == null)
            throw new InvalidOperationException("Could not identify image.");

        stream.Seek(position, SeekOrigin.Begin);

        IPixelBuffer pixelBuffer = null!;
        switch (info.PixelType.BitsPerPixel)
        {
            case 8:
                using (var imageL8 = SixLabors.ImageSharp.Image.Load<SixLabors.ImageSharp.PixelFormats.L8>(_configuration, stream))
                {
                    if (imageL8.DangerousTryGetSinglePixelMemory(out var pixelsL8))
                    {
                        var buffer = MemoryMarshal.Cast<SixLabors.ImageSharp.PixelFormats.L8, Mono8>(pixelsL8.Span);
                        pixelBuffer = new PackedPixelBuffer<Mono8>(imageL8.Width, imageL8.Height, buffer);
                    }
                    else
                    {
                        throw new InvalidOperationException("Could not get pixel buffer.");
                    }
                }
                break;
            case 24:
            case 32:
                using (var imageRgb24 = SixLabors.ImageSharp.Image.Load<SixLabors.ImageSharp.PixelFormats.Rgb24>(_configuration, stream))
                {
                    if (imageRgb24.DangerousTryGetSinglePixelMemory(out var pixelsRgb24))
                    {
                        var buffer = MemoryMarshal.Cast<SixLabors.ImageSharp.PixelFormats.Rgb24, Rgb24>(pixelsRgb24.Span);
                        pixelBuffer = new PackedPixelBuffer<Rgb24>(imageRgb24.Width, imageRgb24.Height, buffer);
                    }
                    else
                    {
                        throw new InvalidOperationException("Could not get pixel buffer.");
                    }
                }
                break;

            default:
                throw new NotSupportedException($"Pixel type {info.PixelType} is not supported.");
        }

        return pixelBuffer;
    }
}