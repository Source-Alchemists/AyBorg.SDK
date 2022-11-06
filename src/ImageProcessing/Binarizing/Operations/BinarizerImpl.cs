using System.Runtime.CompilerServices;
using Autodroid.SDK.ImageProcessing.Buffers;
using Autodroid.SDK.ImageProcessing.Pixels;

namespace Autodroid.SDK.ImageProcessing.Binarizing.Operations;

internal static class BinarizerImpl
{
    [MethodImpl(MethodImplOptions.AggressiveOptimization)]
    public static void BinarizeMono(ReadOnlySpan<Mono> sourceRow, Span<Mono> targetRow, ReadOnlyPackedPixelBuffer<Mono> sourceBuffer, float threshold, BinaryThresholdMode mode)
    {
        var width = sourceBuffer.Width;
        var realThreshold = threshold;
        switch (mode)
        {
            case BinaryThresholdMode.Lumincance:
            case BinaryThresholdMode.Saturation:
            case BinaryThresholdMode.MaxChroma:
                for (int x = 0; x < width; x++)
                {
                    targetRow[x] = sourceRow[x] >= realThreshold ? Mono.White : Mono.Black;
                }
                break;

        }
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization)]
    public static void BinarizeMono8(ReadOnlySpan<Mono8> sourceRow, Span<Mono8> targetRow, ReadOnlyPackedPixelBuffer<Mono8> sourceBuffer, float threshold, BinaryThresholdMode mode)
    {
        var width = sourceBuffer.Width;
        var realThreshold = threshold * Mono8.White;
        switch (mode)
        {
            case BinaryThresholdMode.Lumincance:
            case BinaryThresholdMode.Saturation:
            case BinaryThresholdMode.MaxChroma:
                for (int x = 0; x < width; x++)
                {
                    targetRow[x] = sourceRow[x] >= realThreshold ? Mono8.White : Mono8.Black;
                }
                break;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization)]
    public static void BinarizeMono16(ReadOnlySpan<Mono16> sourceRow, Span<Mono8> targetRow, ReadOnlyPackedPixelBuffer<Mono16> sourceBuffer, float threshold, BinaryThresholdMode mode)
    {
        var width = sourceBuffer.Width;
        var realThreshold = threshold * Mono8.White;
        switch (mode)
        {
            case BinaryThresholdMode.Lumincance:
            case BinaryThresholdMode.Saturation:
            case BinaryThresholdMode.MaxChroma:
                for (int x = 0; x < width; x++)
                {
                    targetRow[x] = sourceRow[x] >= realThreshold ? Mono8.White : Mono8.Black;
                }
                break;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization)]
    public static void BinarizeRgb(ReadOnlySpan<Rgb> sourceRow, Span<Mono> targetRow, ReadOnlyPackedPixelBuffer<Rgb> sourceBuffer, float threshold, BinaryThresholdMode mode)
    {
        var width = sourceBuffer.Width;
        var realThreshold = threshold;
        switch (mode)
        {
            case BinaryThresholdMode.Lumincance:
                for (int x = 0; x < width; x++)
                {
                    var pixel = sourceRow[x];
                    var luminance = ColorNumerics.Get16BitBT709Luminance(pixel.Red, pixel.Green, pixel.Blue);
                    targetRow[x] = luminance >= realThreshold ? Mono8.White : Mono8.Black;
                }
                break;
            case BinaryThresholdMode.Saturation:
                for (int x = 0; x < width; x++)
                {
                    float saturation = ColorNumerics.GetSaturation(sourceRow[x]);
                    ref Mono color = ref targetRow[x];
                    color = saturation >= threshold ? Mono8.White : Mono8.Black;
                }
                break;
            case BinaryThresholdMode.MaxChroma:
                float ct = realThreshold / 2f;
                for (int x = 0; x < width; x++)
                {
                    float maxChroma = ColorNumerics.GetMaxChroma(sourceRow[x]);
                    ref Mono color = ref targetRow[x];
                    color = maxChroma >= ct ? Mono8.White : Mono8.Black;
                }
                break;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization)]
    public static void BinarizeRgb24(ReadOnlySpan<Rgb24> sourceRow, Span<Mono8> targetRow, ReadOnlyPackedPixelBuffer<Rgb24> sourceBuffer, float threshold, BinaryThresholdMode mode)
    {
        var width = sourceBuffer.Width;
        var realThreshold = threshold * Mono8.White;
        switch (mode)
        {
            case BinaryThresholdMode.Lumincance:
                for (int x = 0; x < width; x++)
                {
                    var pixel = sourceRow[x];
                    var luminance = ColorNumerics.Get8BitBT709Luminance(pixel.Red, pixel.Green, pixel.Blue);
                    targetRow[x] = luminance >= realThreshold ? Mono8.White : Mono8.Black;
                }
                break;
            case BinaryThresholdMode.Saturation:
                for (int x = 0; x < width; x++)
                {
                    float saturation = ColorNumerics.GetSaturation(sourceRow[x]);
                    ref Mono8 color = ref targetRow[x];
                    color = saturation >= threshold ? Mono8.White : Mono8.Black;
                }
                break;
            case BinaryThresholdMode.MaxChroma:
                float ct = realThreshold / 2f;
                for (int x = 0; x < width; x++)
                {
                    float maxChroma = ColorNumerics.GetMaxChroma(sourceRow[x]);
                    ref Mono8 color = ref targetRow[x];
                    color = maxChroma >= ct ? Mono8.White : Mono8.Black;
                }
                break;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization)]
    public static void BinarizeRgb48(ReadOnlySpan<Rgb48> sourceRow, Span<Mono8> targetRow, ReadOnlyPackedPixelBuffer<Rgb48> sourceBuffer, float threshold, BinaryThresholdMode mode)
    {
        var width = sourceBuffer.Width;
        var realThreshold = threshold * Mono8.White;
        switch (mode)
        {
            case BinaryThresholdMode.Lumincance:
                for (int x = 0; x < width; x++)
                {
                    var pixel = sourceRow[x];
                    var luminance = ColorNumerics.Get16BitBT709Luminance(pixel.Red, pixel.Green, pixel.Blue);
                    targetRow[x] = luminance >= realThreshold ? Mono8.White : Mono8.Black;
                }
                break;
            case BinaryThresholdMode.Saturation:
                for (int x = 0; x < width; x++)
                {
                    float saturation = ColorNumerics.GetSaturation(sourceRow[x]);
                    ref Mono8 color = ref targetRow[x];
                    color = saturation >= threshold ? Mono8.White : Mono8.Black;
                }
                break;
            case BinaryThresholdMode.MaxChroma:
                float ct = realThreshold / 2f;
                for (int x = 0; x < width; x++)
                {
                    float maxChroma = ColorNumerics.GetMaxChroma(sourceRow[x]);
                    ref Mono8 color = ref targetRow[x];
                    color = maxChroma >= ct ? Mono8.White : Mono8.Black;
                }
                break;
        }
    }
}