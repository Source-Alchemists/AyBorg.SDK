using System.Runtime.CompilerServices;
using Atomy.SDK.ImageProcessing.Buffers;
using Atomy.SDK.ImageProcessing.Pixels;

namespace Atomy.SDK.ImageProcessing.Converting.Operations;

public class GrayscaleOperation : GrayscaleConverter
{
    public GrayscaleOperation()
    {
        AddOperation<ReadOnlyPackedPixelBuffer<Rgb>, PackedPixelBuffer<Mono>>(RgbToMono);
        AddOperation<ReadOnlyPackedPixelBuffer<Rgb24>, PackedPixelBuffer<Mono8>>(Rgb24ToMono8);
        AddOperation<ReadOnlyPackedPixelBuffer<Rgb48>, PackedPixelBuffer<Mono16>>(Rgb48ToMono16);

        AddOperation<ReadOnlyPlanarPixelBuffer<RgbFFF>, PackedPixelBuffer<Mono>>(RgbFFFToMono);
        AddOperation<ReadOnlyPlanarPixelBuffer<Rgb888>, PackedPixelBuffer<Mono8>>(Rgb888ToMono8);
        AddOperation<ReadOnlyPlanarPixelBuffer<Rgb161616>, PackedPixelBuffer<Mono16>>(Rgb161616ToMono16);
    }

    private static PackedPixelBuffer<Mono> RgbToMono(GrayscaleConverterParameters parameters)
    {
        var sourcePixelBuffer = (ReadOnlyPackedPixelBuffer<Rgb>)parameters.Input;
        var targetPixelBuffer = new PackedPixelBuffer<Mono>(parameters.Input.Width, parameters.Input.Height);
        Parallel.For(0, targetPixelBuffer.Height, parameters.ParallelOptions, row =>
        {
            var sourceRow = sourcePixelBuffer.GetRow(row);
            var targetRow = targetPixelBuffer.GetRow(row);
            for (int column = 0; column < targetPixelBuffer.Width; column++)
            {
                var sourcePixel = sourceRow[column];
                targetRow[column] = ToGrayscale(sourcePixel.Red, sourcePixel.Green, sourcePixel.Blue);
            }
        });
        return targetPixelBuffer;
    }

    private static PackedPixelBuffer<Mono8> Rgb24ToMono8(GrayscaleConverterParameters parameters)
    {
        var sourcePixelBuffer = (ReadOnlyPackedPixelBuffer<Rgb24>)parameters.Input;
        var targetPixelBuffer = new PackedPixelBuffer<Mono8>(parameters.Input.Width, parameters.Input.Height);
        Parallel.For(0, targetPixelBuffer.Height, parameters.ParallelOptions, row =>
        {
            var sourceRow = sourcePixelBuffer.GetRow(row);
            var targetRow = targetPixelBuffer.GetRow(row);
            for (int column = 0; column < targetPixelBuffer.Width; column++)
            {
                var sourcePixel = sourceRow[column];
                targetRow[column] = ToGrayscale(sourcePixel.Red, sourcePixel.Green, sourcePixel.Blue);
            }
        });
        return targetPixelBuffer;
    }

    private static PackedPixelBuffer<Mono16> Rgb48ToMono16(GrayscaleConverterParameters parameters)
    {
        var sourcePixelBuffer = (ReadOnlyPackedPixelBuffer<Rgb48>)parameters.Input;
        var targetPixelBuffer = new PackedPixelBuffer<Mono16>(parameters.Input.Width, parameters.Input.Height);
        Parallel.For(0, targetPixelBuffer.Height, parameters.ParallelOptions, row =>
        {
            var sourceRow = sourcePixelBuffer.GetRow(row);
            var targetRow = targetPixelBuffer.GetRow(row);
            for (int column = 0; column < targetPixelBuffer.Width; column++)
            {
                var sourcePixel = sourceRow[column];
                targetRow[column] = ToGrayscale(sourcePixel.Red, sourcePixel.Green, sourcePixel.Blue);
            }
        });
        return targetPixelBuffer;
    }

    private static PackedPixelBuffer<Mono> RgbFFFToMono(GrayscaleConverterParameters parameters)
    {
        var sourcePixelBuffer = (ReadOnlyPlanarPixelBuffer<RgbFFF>)parameters.Input;
        var targetPixelBuffer = new PackedPixelBuffer<Mono>(parameters.Input.Width, parameters.Input.Height);
        Parallel.For(0, targetPixelBuffer.Height, parameters.ParallelOptions, row =>
        {
            var sourceRowRed = sourcePixelBuffer.GetRow(0, row);
            var sourceRowGreen = sourcePixelBuffer.GetRow(1, row);
            var sourceRowBlue = sourcePixelBuffer.GetRow(2, row);
            var targetRow = targetPixelBuffer.GetRow(row);
            for (int column = 0; column < targetPixelBuffer.Width; column++)
            {
                targetRow[column] = ToGrayscale(sourceRowRed[column], sourceRowGreen[column], sourceRowBlue[column]);
            }
        });
        return targetPixelBuffer;
    }

    private static PackedPixelBuffer<Mono8> Rgb888ToMono8(GrayscaleConverterParameters parameters)
    {
        var sourcePixelBuffer = (ReadOnlyPlanarPixelBuffer<Rgb888>)parameters.Input;
        var targetPixelBuffer = new PackedPixelBuffer<Mono8>(parameters.Input.Width, parameters.Input.Height);
        Parallel.For(0, targetPixelBuffer.Height, parameters.ParallelOptions, row =>
        {
            var sourceRowRed = sourcePixelBuffer.GetRow(0, row);
            var sourceRowGreen = sourcePixelBuffer.GetRow(1, row);
            var sourceRowBlue = sourcePixelBuffer.GetRow(2, row);
            var targetRow = targetPixelBuffer.GetRow(row);
            for (int column = 0; column < targetPixelBuffer.Width; column++)
            {
                targetRow[column] = ToGrayscale(sourceRowRed[column], sourceRowGreen[column], sourceRowBlue[column]);
            }
        });
        return targetPixelBuffer;
    }

    private static PackedPixelBuffer<Mono16> Rgb161616ToMono16(GrayscaleConverterParameters parameters)
    {
        var sourcePixelBuffer = (ReadOnlyPlanarPixelBuffer<Rgb161616>)parameters.Input;
        var targetPixelBuffer = new PackedPixelBuffer<Mono16>(parameters.Input.Width, parameters.Input.Height);
        Parallel.For(0, targetPixelBuffer.Height, parameters.ParallelOptions, row =>
        {
            var sourceRowRed = sourcePixelBuffer.GetRow(0, row);
            var sourceRowGreen = sourcePixelBuffer.GetRow(1, row);
            var sourceRowBlue = sourcePixelBuffer.GetRow(2, row);
            var targetRow = targetPixelBuffer.GetRow(row);
            for (int column = 0; column < targetPixelBuffer.Width; column++)
            {
                targetRow[column] = ToGrayscale(sourceRowRed[column], sourceRowGreen[column], sourceRowBlue[column]);
            }
        });
        return targetPixelBuffer;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    private static float ToGrayscale(float red, float green, float blue) 
                            => (red * .299f) + (green * .587f) + (blue * .114f);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    private static ushort ToGrayscale(ushort red, ushort green, ushort blue) 
                            => (ushort)((red * 299 + green * 587 + blue * 114) / 1000);

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    private static byte ToGrayscale(byte red, byte green, byte blue) 
                            => (byte)((red * 299 + green * 587 + blue * 114) / 1000);
}