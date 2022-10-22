using Atomy.SDK.ImageProcessing.Buffers;
using Atomy.SDK.ImageProcessing.Pixels;

namespace Atomy.SDK.ImageProcessing.Binarizing.Operations;

public sealed class BinarizerOperation : Binarizer
{
    public BinarizerOperation()
    {
        AddOperation<ReadOnlyPackedPixelBuffer<Mono>, PackedPixelBuffer<Mono>>(BinarizeMono);
        AddOperation<ReadOnlyPackedPixelBuffer<Mono8>, PackedPixelBuffer<Mono8>>(BinarizeMono8);
        AddOperation<ReadOnlyPackedPixelBuffer<Mono16>, PackedPixelBuffer<Mono8>>(BinarizeMono16);

        AddOperation<ReadOnlyPackedPixelBuffer<Rgb>, PackedPixelBuffer<Mono>>(BinarizeRgb);
        AddOperation<ReadOnlyPackedPixelBuffer<Rgb24>, PackedPixelBuffer<Mono8>>(BinarizeRgb24);
        AddOperation<ReadOnlyPackedPixelBuffer<Rgb48>, PackedPixelBuffer<Mono8>>(BinarizeRgb48);
    }

    private static PackedPixelBuffer<Mono> BinarizeMono(BinarizerParameters parameters)
    {
        var sourceBuffer = (ReadOnlyPackedPixelBuffer<Mono>)parameters.Input!;
        var targetBuffer = new PackedPixelBuffer<Mono>(sourceBuffer.Width, sourceBuffer.Height);
        Parallel.For(0, sourceBuffer.Height, parameters.ParallelOptions, rowIndex =>
        {
            BinarizerImpl.BinarizeMono(sourceBuffer.GetRow(rowIndex), targetBuffer.GetRow(rowIndex),
                                            sourceBuffer, rowIndex, parameters.Threshold, parameters.Mode);
        });
        return targetBuffer;
    }

    private static PackedPixelBuffer<Mono8> BinarizeMono8(BinarizerParameters parameters)
    {
        var sourceBuffer = (ReadOnlyPackedPixelBuffer<Mono8>)parameters.Input!;
        var targetBuffer = new PackedPixelBuffer<Mono8>(sourceBuffer.Width, sourceBuffer.Height);
        Parallel.For(0, sourceBuffer.Height, parameters.ParallelOptions, rowIndex =>
        {
            BinarizerImpl.BinarizeMono8(sourceBuffer.GetRow(rowIndex), targetBuffer.GetRow(rowIndex),
                                            sourceBuffer, rowIndex, parameters.Threshold, parameters.Mode);
        });
        return targetBuffer;
    }

    private static PackedPixelBuffer<Mono8> BinarizeMono16(BinarizerParameters parameters)
    {
        var sourceBuffer = (ReadOnlyPackedPixelBuffer<Mono16>)parameters.Input!;
        var targetBuffer = new PackedPixelBuffer<Mono8>(sourceBuffer.Width, sourceBuffer.Height);
        Parallel.For(0, sourceBuffer.Height, parameters.ParallelOptions, rowIndex =>
        {
            BinarizerImpl.BinarizeMono16(sourceBuffer.GetRow(rowIndex), targetBuffer.GetRow(rowIndex),
                                            sourceBuffer, rowIndex, parameters.Threshold, parameters.Mode);
        });
        return targetBuffer;
    }

    private static PackedPixelBuffer<Mono> BinarizeRgb(BinarizerParameters parameters)
    {
        var sourceBuffer = (ReadOnlyPackedPixelBuffer<Rgb>)parameters.Input!;
        var targetBuffer = new PackedPixelBuffer<Mono>(sourceBuffer.Width, sourceBuffer.Height);
        Parallel.For(0, sourceBuffer.Height, parameters.ParallelOptions, rowIndex =>
        {
            BinarizerImpl.BinarizeRgb(sourceBuffer.GetRow(rowIndex), targetBuffer.GetRow(rowIndex),
                                            sourceBuffer, rowIndex, parameters.Threshold, parameters.Mode);
        });
        return targetBuffer;
    }

    private static PackedPixelBuffer<Mono8> BinarizeRgb24(BinarizerParameters parameters)
    {
        var sourceBuffer = (ReadOnlyPackedPixelBuffer<Rgb24>)parameters.Input!;
        var targetBuffer = new PackedPixelBuffer<Mono8>(sourceBuffer.Width, sourceBuffer.Height);
        Parallel.For(0, sourceBuffer.Height, parameters.ParallelOptions, rowIndex =>
        {
            BinarizerImpl.BinarizeRgb24(sourceBuffer.GetRow(rowIndex), targetBuffer.GetRow(rowIndex),
                                            sourceBuffer, rowIndex, parameters.Threshold, parameters.Mode);
        });
        return targetBuffer;
    }

    private static PackedPixelBuffer<Mono8> BinarizeRgb48(BinarizerParameters parameters)
    {
        var sourceBuffer = (ReadOnlyPackedPixelBuffer<Rgb48>)parameters.Input!;
        var targetBuffer = new PackedPixelBuffer<Mono8>(sourceBuffer.Width, sourceBuffer.Height);
        Parallel.For(0, sourceBuffer.Height, parameters.ParallelOptions, rowIndex =>
        {
            BinarizerImpl.BinarizeRgb48(sourceBuffer.GetRow(rowIndex), targetBuffer.GetRow(rowIndex),
                                            sourceBuffer, rowIndex, parameters.Threshold, parameters.Mode);
        });
        return targetBuffer;
    }
}