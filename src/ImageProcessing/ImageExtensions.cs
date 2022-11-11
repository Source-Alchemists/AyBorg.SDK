using Autodroid.SDK.ImageProcessing.Binarizing;
using Autodroid.SDK.ImageProcessing.Buffers;
using Autodroid.SDK.ImageProcessing.Converting;
using Autodroid.SDK.ImageProcessing.Cropping;
using Autodroid.SDK.ImageProcessing.Mirroring;
using Autodroid.SDK.ImageProcessing.Pixels;
using Autodroid.SDK.ImageProcessing.Resizing;
using Autodroid.SDK.ImageProcessing.Shapes;

namespace Autodroid.SDK.ImageProcessing;

public static class ImageExtensions
{
    private static readonly Resizer _resizer = new();
    private static readonly GrayscaleConverter _grayscaleConverter = new();
    private static readonly Mirror _mirror = new();
    private static readonly Crop _crop = new();
    private static readonly Binarizer _binarizer = new();

    /// <summary>
    /// Resizes the image.
    /// </summary>
    /// <param name="image">The image.</param>
    /// <param name="width">The width.</param>
    /// <param name="height">The height.</param>
    /// <param name="resizeMode">The resize mode.</param>
    /// <returns>The resized image.</returns>
    public static Image Resize(this Image image, int width, int height, ResizeMode resizeMode = ResizeMode.Bicubic)
    {
        if (width < 1)
            throw new ArgumentOutOfRangeException(nameof(width), "Width must be greater than 0.");
        if (height < 1)
            throw new ArgumentOutOfRangeException(nameof(height), "Height must be greater than 0.");

        IReadOnlyPixelBuffer sourceBuffer = image.GetPixelBuffer();
        var resizedPixelBuffer = _resizer.Execute(new Resizing.Operations.ResizerParameters
        {
            Input = sourceBuffer,
            Width = width,
            Height = height,
            ResizeMode = resizeMode
        });

        return new Image(resizedPixelBuffer);
    }

    /// <summary>
    /// Converts the image to grayscale.
    /// </summary>
    /// <param name="image">The image.</param>
    /// <returns>The grayscale image.</returns>
    public static Image ToGrayscale(this Image image)
    {
        if (!image.IsColor)
        {
            return new Image(image);
        }

        IReadOnlyPixelBuffer sourceBuffer = null!;
        Type targetType = null!;
        switch (image.PixelFormat)
        {
            case PixelFormat.RgbPacked:
                sourceBuffer = image.AsPacked<Rgb>();
                targetType = typeof(PackedPixelBuffer<Mono>);
                break;
            case PixelFormat.Rgb24Packed:
                sourceBuffer = image.AsPacked<Rgb24>();
                targetType = typeof(PackedPixelBuffer<Mono8>);
                break;
            case PixelFormat.Rgb48Packed:
                sourceBuffer = image.AsPacked<Rgb48>();
                targetType = typeof(PackedPixelBuffer<Mono16>);
                break;
            case PixelFormat.RgbPlanar:
                sourceBuffer = image.AsPlanar<RgbFFF>();
                targetType = typeof(PackedPixelBuffer<Mono>);
                break;
            case PixelFormat.Rgb888Planar:
                sourceBuffer = image.AsPlanar<Rgb888>();
                targetType = typeof(PackedPixelBuffer<Mono8>);
                break;
            case PixelFormat.Rgb161616Planar:
                sourceBuffer = image.AsPlanar<Rgb161616>();
                targetType = typeof(PackedPixelBuffer<Mono16>);
                break;
        }
        var grayscaleBuffer = _grayscaleConverter.Execute(new Converting.Operations.GrayscaleConverterParameters
        {
            Input = sourceBuffer,
            OutputType = targetType
        });

        return new Image(grayscaleBuffer);
    }

    /// <summary>
    /// Mirror the image.
    /// </summary>
    /// <param name="image">The image.</param>
    /// <param name="mirrorMode">The mirror mode.</param>
    /// <returns>The mirrored image.</returns>
    public static Image Mirror(this Image image, MirrorMode mirrorMode)
    {
        IReadOnlyPixelBuffer sourceBuffer = image.GetPixelBuffer();
        var mirroredBuffer = _mirror.Execute(new Mirroring.Operations.MirrorParameters
        {
            Input = sourceBuffer,
            MirrorMode = mirrorMode
        });

        return new Image(mirroredBuffer);
    }

    /// <summary>
    /// Crop the image.
    /// </summary>
    /// <param name="image">The image.</param>
    /// <param name="rectangle">The rectangle/region to crop.</param>
    /// <returns>The cropped image.</returns>
    public static Image Crop(this Image image, Rectangle rectangle)
    {
        IReadOnlyPixelBuffer sourceBuffer = image.GetPixelBuffer();
        var croppedBuffer = _crop.Execute(new Cropping.Operations.CropParameters
        {
            Input = sourceBuffer,
            Rectangle = rectangle
        });

        return new Image(croppedBuffer);
    }

    /// <summary>
    /// Binary threshold the image.
    /// </summary>
    /// <param name="image">The image.</param>
    /// <param name="threshold">The threshold.</param>
    /// <param name="mode">The threshold mode.</param>
    /// <returns>The binary thresholded image.</returns>
    public static Image Binarize(this Image image, float threshold = 0.5f, BinaryThresholdMode mode = BinaryThresholdMode.Lumincance)
    {
        IReadOnlyPixelBuffer sourceBuffer = image.PixelFormat switch
        {
            PixelFormat.RgbPlanar => image.AsPacked<Rgb>(),
            PixelFormat.Rgb888Planar => image.AsPacked<Rgb24>(),
            PixelFormat.Rgb161616Planar => image.AsPacked<Rgb48>(),
            _ => image.GetPixelBuffer(),
        };
        var binarizedBuffer = _binarizer.Execute(new Binarizing.Operations.BinarizerParameters
        {
            Input = sourceBuffer,
            Threshold = threshold,
            Mode = mode
        });

        return new Image(binarizedBuffer);
    }

    internal static IReadOnlyPixelBuffer GetPixelBuffer(this Image image)
    {
        return image.PixelFormat switch
        {
            PixelFormat.Mono => image.AsPacked<Mono>(),
            PixelFormat.Mono8 => image.AsPacked<Mono8>(),
            PixelFormat.Mono16 => image.AsPacked<Mono16>(),
            PixelFormat.RgbPacked => image.AsPacked<Rgb>(),
            PixelFormat.Rgb24Packed => image.AsPacked<Rgb24>(),
            PixelFormat.Rgb48Packed => image.AsPacked<Rgb48>(),
            PixelFormat.RgbPlanar => image.AsPlanar<RgbFFF>(),
            PixelFormat.Rgb888Planar => image.AsPlanar<Rgb888>(),
            PixelFormat.Rgb161616Planar => image.AsPlanar<Rgb161616>(),
            _ => throw new NotSupportedException($"The pixel format {image.PixelFormat} is not supported."),
        };
    }
}