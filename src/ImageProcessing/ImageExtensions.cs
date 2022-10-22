using Atomy.SDK.ImageProcessing.Binarizing;
using Atomy.SDK.ImageProcessing.Buffers;
using Atomy.SDK.ImageProcessing.Converting;
using Atomy.SDK.ImageProcessing.Cropping;
using Atomy.SDK.ImageProcessing.Mirroring;
using Atomy.SDK.ImageProcessing.Pixels;
using Atomy.SDK.ImageProcessing.Resizing;
using Atomy.SDK.ImageProcessing.Shapes;

namespace Atomy.SDK.ImageProcessing;

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
        if(width < 1)
            throw new ArgumentOutOfRangeException(nameof(width), "Width must be greater than 0.");
        if(height < 1)
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
        if(!image.IsColor)
        {
            return new Image(image);
        }

        IReadOnlyPixelBuffer sourceBuffer = null!;
        Type targetType = null!;
        switch(image.PixelFormat)
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
        IReadOnlyPixelBuffer sourceBuffer;

        switch(image.PixelFormat)
        {
            case PixelFormat.RgbPlanar:
                sourceBuffer = image.AsPacked<Rgb>();
                break;
            case PixelFormat.Rgb888Planar:
                sourceBuffer = image.AsPacked<Rgb24>();
                break;
            case PixelFormat.Rgb161616Planar:
                sourceBuffer = image.AsPacked<Rgb48>();
                break;
            default:
                sourceBuffer = image.GetPixelBuffer();
                break;

        }

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
        switch(image.PixelFormat)
        {
            case PixelFormat.Mono:
                return image.AsPacked<Mono>();
            case PixelFormat.Mono8:
                return image.AsPacked<Mono8>();
            case PixelFormat.Mono16:
                return image.AsPacked<Mono16>();
            case PixelFormat.RgbPacked:
                return image.AsPacked<Rgb>();
            case PixelFormat.Rgb24Packed:
                return image.AsPacked<Rgb24>();
            case PixelFormat.Rgb48Packed:
                return image.AsPacked<Rgb48>();
            case PixelFormat.RgbPlanar:
                return image.AsPlanar<RgbFFF>();
            case PixelFormat.Rgb888Planar:
                return image.AsPlanar<Rgb888>();
            case PixelFormat.Rgb161616Planar:
                return image.AsPlanar<Rgb161616>();
            default:
                throw new NotSupportedException($"The pixel format {image.PixelFormat} is not supported.");
        }
    }
}