using Autodroid.SDK.ImageProcessing.Buffers;
using Autodroid.SDK.ImageProcessing.Pixels;

namespace Autodroid.SDK.ImageProcessing.Resizing.Operations;

public sealed class ResizeOperation : Resizer
{
    public ResizeOperation()
    {
        AddOperation<ReadOnlyPackedPixelBuffer<Mono>, PackedPixelBuffer<Mono>>(ResizeMono);
        AddOperation<ReadOnlyPackedPixelBuffer<Mono8>, PackedPixelBuffer<Mono8>>(ResizeMono8);
        AddOperation<ReadOnlyPackedPixelBuffer<Mono16>, PackedPixelBuffer<Mono16>>(ResizeMono16);

        AddOperation<ReadOnlyPackedPixelBuffer<Rgb>, PackedPixelBuffer<Rgb>>(ResizeRgb);
        AddOperation<ReadOnlyPackedPixelBuffer<Rgb24>, PackedPixelBuffer<Rgb24>>(ResizeRgb24);
        AddOperation<ReadOnlyPackedPixelBuffer<Rgb48>, PackedPixelBuffer<Rgb48>>(ResizeRgb48);

        AddOperation<ReadOnlyPlanarPixelBuffer<RgbFFF>, PlanarPixelBuffer<RgbFFF>>(ResizeRgbFFF);
        AddOperation<ReadOnlyPlanarPixelBuffer<Rgb888>, PlanarPixelBuffer<Rgb888>>(ResizeRgb888);
        AddOperation<ReadOnlyPlanarPixelBuffer<Rgb161616>, PlanarPixelBuffer<Rgb161616>>(ResizeRgb161616);
    }

    private static PackedPixelBuffer<Mono> ResizeMono(ResizerParameters parameters)
    {
        var targetPixelBuffer = new PackedPixelBuffer<Mono>(parameters.Width, parameters.Height);
        var sourceBuffer = (ReadOnlyPackedPixelBuffer<Mono>)parameters.Input!;
        switch (parameters.ResizeMode)
        {
            case ResizeMode.NearestNeighbor:
                Parallel.For(0, targetPixelBuffer.Height, parameters.ParallelOptions, row =>
                {
                    ResizeImpl.ResizeNearestNeighborSingle(sourceBuffer.Pixels.AsSingle(), 
                                                            targetPixelBuffer.Pixels.AsSingle(), 
                                                            sourceBuffer.Width, 
                                                            sourceBuffer.Height, 
                                                            targetPixelBuffer.Width, 
                                                            targetPixelBuffer.Height, 
                                                            row);
                });
                break;
            case ResizeMode.Bilinear:
                Parallel.For(0, targetPixelBuffer.Height, parameters.ParallelOptions, row =>
                {
                    ResizeImpl.ResizeBilinearSingle(sourceBuffer.Pixels.AsSingle(), 
                                                    targetPixelBuffer.Pixels.AsSingle(), 
                                                    sourceBuffer.Width, 
                                                    sourceBuffer.Height, 
                                                    targetPixelBuffer.Width, 
                                                    targetPixelBuffer.Height, 
                                                    row);
                });
                break;
            case ResizeMode.Bicubic:
                Parallel.For(0, targetPixelBuffer.Height, parameters.ParallelOptions, row =>
                {
                    ResizeImpl.ResizeBicubicSingle(sourceBuffer.Pixels.AsSingle(), 
                                                    targetPixelBuffer.Pixels.AsSingle(), 
                                                    sourceBuffer.Width, 
                                                    sourceBuffer.Height, 
                                                    targetPixelBuffer.Width, 
                                                    targetPixelBuffer.Height, 
                                                    row);
                });
                break;
        }

        return targetPixelBuffer;
    }

    private static PackedPixelBuffer<Mono8> ResizeMono8(ResizerParameters parameters)
    {
        var targetPixelBuffer = new PackedPixelBuffer<Mono8>(parameters.Width, parameters.Height);
        var sourceBuffer = (ReadOnlyPackedPixelBuffer<Mono8>)parameters.Input!;
        switch (parameters.ResizeMode)
        {
            case ResizeMode.NearestNeighbor:
                Parallel.For(0, targetPixelBuffer.Height, parameters.ParallelOptions, row =>
                {
                    ResizeImpl.ResizeNearestNeighborByte(sourceBuffer.Pixels.AsByte(), 
                                                            targetPixelBuffer.Pixels.AsByte(), 
                                                            sourceBuffer.Width, 
                                                            sourceBuffer.Height, 
                                                            targetPixelBuffer.Width, 
                                                            targetPixelBuffer.Height, 
                                                            row);
                });
                break;
            case ResizeMode.Bilinear:
                Parallel.For(0, targetPixelBuffer.Height, parameters.ParallelOptions, row =>
                {
                    ResizeImpl.ResizeBilinearByte(sourceBuffer.Pixels.AsByte(), 
                                                    targetPixelBuffer.Pixels.AsByte(), 
                                                    sourceBuffer.Width, 
                                                    sourceBuffer.Height, 
                                                    targetPixelBuffer.Width, 
                                                    targetPixelBuffer.Height, 
                                                    row);
                });
                break;
            case ResizeMode.Bicubic:
                Parallel.For(0, targetPixelBuffer.Height, parameters.ParallelOptions, row =>
                {
                    ResizeImpl.ResizeBicubicByte(sourceBuffer.Pixels.AsByte(), 
                                                    targetPixelBuffer.Pixels.AsByte(), 
                                                    sourceBuffer.Width, 
                                                    sourceBuffer.Height, 
                                                    targetPixelBuffer.Width, 
                                                    targetPixelBuffer.Height, 
                                                    row);
                });
                break;
        }

        return targetPixelBuffer;
    }

    private static PackedPixelBuffer<Mono16> ResizeMono16(ResizerParameters parameters)
    {
        var targetPixelBuffer = new PackedPixelBuffer<Mono16>(parameters.Width, parameters.Height);
        var sourceBuffer = (ReadOnlyPackedPixelBuffer<Mono16>)parameters.Input!;
        switch (parameters.ResizeMode)
        {
            case ResizeMode.NearestNeighbor:
                Parallel.For(0, targetPixelBuffer.Height, parameters.ParallelOptions, row =>
                {
                    ResizeImpl.ResizeNearestNeighborUInt16(sourceBuffer.Pixels.AsUInt16(), 
                                                            targetPixelBuffer.Pixels.AsUInt16(), 
                                                            sourceBuffer.Width, 
                                                            sourceBuffer.Height, 
                                                            targetPixelBuffer.Width, 
                                                            targetPixelBuffer.Height, 
                                                            row);
                });
                break;
            case ResizeMode.Bilinear:
                Parallel.For(0, targetPixelBuffer.Height, parameters.ParallelOptions, row =>
                {
                    ResizeImpl.ResizeBilinearUInt16(sourceBuffer.Pixels.AsUInt16(), 
                                                    targetPixelBuffer.Pixels.AsUInt16(), 
                                                    sourceBuffer.Width, 
                                                    sourceBuffer.Height, 
                                                    targetPixelBuffer.Width, 
                                                    targetPixelBuffer.Height, 
                                                    row);
                });
                break;
            case ResizeMode.Bicubic:
                Parallel.For(0, targetPixelBuffer.Height, parameters.ParallelOptions, row =>
                {
                    ResizeImpl.ResizeBicubicUInt16(sourceBuffer.Pixels.AsUInt16(), 
                                                    targetPixelBuffer.Pixels.AsUInt16(), 
                                                    sourceBuffer.Width, 
                                                    sourceBuffer.Height, 
                                                    targetPixelBuffer.Width, 
                                                    targetPixelBuffer.Height, 
                                                    row);
                });
                break;
        }

        return targetPixelBuffer;
    }

    private static PackedPixelBuffer<Rgb> ResizeRgb(ResizerParameters parameters)
    {
        var targetPixelBuffer = new PackedPixelBuffer<Rgb>(parameters.Width, parameters.Height);
        var sourceBuffer = (ReadOnlyPackedPixelBuffer<Rgb>)parameters.Input!;
        switch (parameters.ResizeMode)
        {
            case ResizeMode.NearestNeighbor:
                Parallel.For(0, targetPixelBuffer.Height, parameters.ParallelOptions, row =>
                {
                    ResizeImpl.ResizeNearestNeighborRgb(sourceBuffer.Pixels, 
                                                            targetPixelBuffer.Pixels, 
                                                            sourceBuffer.Width, 
                                                            sourceBuffer.Height, 
                                                            targetPixelBuffer.Width, 
                                                            targetPixelBuffer.Height, 
                                                            row);
                });
                break;
            case ResizeMode.Bilinear:
                Parallel.For(0, targetPixelBuffer.Height, parameters.ParallelOptions, row =>
                {
                    ResizeImpl.ResizeBilinearRgb(sourceBuffer.Pixels, 
                                                    targetPixelBuffer.Pixels, 
                                                    sourceBuffer.Width, 
                                                    sourceBuffer.Height, 
                                                    targetPixelBuffer.Width, 
                                                    targetPixelBuffer.Height, 
                                                    row);
                });
                break;
            case ResizeMode.Bicubic:
                Parallel.For(0, targetPixelBuffer.Height, parameters.ParallelOptions, row =>
                {
                    ResizeImpl.ResizeBicubicRgb(sourceBuffer.Pixels, 
                                                    targetPixelBuffer.Pixels, 
                                                    sourceBuffer.Width, 
                                                    sourceBuffer.Height, 
                                                    targetPixelBuffer.Width, 
                                                    targetPixelBuffer.Height, 
                                                    row);
                });
                break;
        }

        return targetPixelBuffer;
    }

    private static PackedPixelBuffer<Rgb24> ResizeRgb24(ResizerParameters parameters)
    {
        var targetPixelBuffer = new PackedPixelBuffer<Rgb24>(parameters.Width, parameters.Height);
        var sourceBuffer = (ReadOnlyPackedPixelBuffer<Rgb24>)parameters.Input!;
        switch (parameters.ResizeMode)
        {
            case ResizeMode.NearestNeighbor:
                Parallel.For(0, targetPixelBuffer.Height, parameters.ParallelOptions, row =>
                {
                    ResizeImpl.ResizeNearestNeighborRgb24(sourceBuffer.Pixels, 
                                                            targetPixelBuffer.Pixels, 
                                                            sourceBuffer.Width, 
                                                            sourceBuffer.Height, 
                                                            targetPixelBuffer.Width, 
                                                            targetPixelBuffer.Height, 
                                                            row);
                });
                break;
            case ResizeMode.Bilinear:
                Parallel.For(0, targetPixelBuffer.Height, parameters.ParallelOptions, row =>
                {
                    ResizeImpl.ResizeBilinearRgb24(sourceBuffer.Pixels, 
                                                    targetPixelBuffer.Pixels, 
                                                    sourceBuffer.Width, 
                                                    sourceBuffer.Height, 
                                                    targetPixelBuffer.Width, 
                                                    targetPixelBuffer.Height, 
                                                    row);
                });
                break;
            case ResizeMode.Bicubic:
                Parallel.For(0, targetPixelBuffer.Height, parameters.ParallelOptions, row =>
                {
                    ResizeImpl.ResizeBicubicRgb24(sourceBuffer.Pixels, 
                                                    targetPixelBuffer.Pixels, 
                                                    sourceBuffer.Width, 
                                                    sourceBuffer.Height, 
                                                    targetPixelBuffer.Width, 
                                                    targetPixelBuffer.Height, 
                                                    row);
                });
                break;
        }

        return targetPixelBuffer;
    }

    private static PackedPixelBuffer<Rgb48> ResizeRgb48(ResizerParameters parameters)
    {
        var targetPixelBuffer = new PackedPixelBuffer<Rgb48>(parameters.Width, parameters.Height);
        var sourceBuffer = (ReadOnlyPackedPixelBuffer<Rgb48>)parameters.Input!;
        switch (parameters.ResizeMode)
        {
            case ResizeMode.NearestNeighbor:
                Parallel.For(0, targetPixelBuffer.Height, parameters.ParallelOptions, row =>
                {
                    ResizeImpl.ResizeNearestNeighborRgb48(sourceBuffer.Pixels, 
                                                            targetPixelBuffer.Pixels, 
                                                            sourceBuffer.Width, 
                                                            sourceBuffer.Height, 
                                                            targetPixelBuffer.Width, 
                                                            targetPixelBuffer.Height, 
                                                            row);
                });
                break;
            case ResizeMode.Bilinear:
                Parallel.For(0, targetPixelBuffer.Height, parameters.ParallelOptions, row =>
                {
                    ResizeImpl.ResizeBilinearRgb48(sourceBuffer.Pixels, 
                                                    targetPixelBuffer.Pixels, 
                                                    sourceBuffer.Width, 
                                                    sourceBuffer.Height, 
                                                    targetPixelBuffer.Width, 
                                                    targetPixelBuffer.Height, 
                                                    row);
                });
                break;
            case ResizeMode.Bicubic:
                Parallel.For(0, targetPixelBuffer.Height, parameters.ParallelOptions, row =>
                {
                    ResizeImpl.ResizeBicubicRgb48(sourceBuffer.Pixels, 
                                                    targetPixelBuffer.Pixels, 
                                                    sourceBuffer.Width, 
                                                    sourceBuffer.Height, 
                                                    targetPixelBuffer.Width, 
                                                    targetPixelBuffer.Height, 
                                                    row);
                });
                break;
        }

        return targetPixelBuffer;
    }

    private static PlanarPixelBuffer<RgbFFF> ResizeRgbFFF(ResizerParameters parameters)
    {
        var targetPixelBuffer = new PlanarPixelBuffer<RgbFFF>(parameters.Width, parameters.Height);
        var sourceBuffer = (ReadOnlyPlanarPixelBuffer<RgbFFF>)parameters.Input!;
        switch (parameters.ResizeMode)
        {
            case ResizeMode.NearestNeighbor:
                Parallel.For(0, targetPixelBuffer.Height, parameters.ParallelOptions, row =>
                {
                    ResizeImpl.ResizeNearestNeighborSingle(sourceBuffer.GetChannel(0).AsSingle(), 
                                                            targetPixelBuffer.GetChannel(0).AsSingle(), 
                                                            sourceBuffer.Width, 
                                                            sourceBuffer.Height, 
                                                            targetPixelBuffer.Width, 
                                                            targetPixelBuffer.Height, 
                                                            row);
                    ResizeImpl.ResizeNearestNeighborSingle(sourceBuffer.GetChannel(1).AsSingle(), 
                                                            targetPixelBuffer.GetChannel(1).AsSingle(), 
                                                            sourceBuffer.Width, 
                                                            sourceBuffer.Height, 
                                                            targetPixelBuffer.Width, 
                                                            targetPixelBuffer.Height, 
                                                            row);
                    ResizeImpl.ResizeNearestNeighborSingle(sourceBuffer.GetChannel(2).AsSingle(), 
                                                            targetPixelBuffer.GetChannel(2).AsSingle(), 
                                                            sourceBuffer.Width, 
                                                            sourceBuffer.Height, 
                                                            targetPixelBuffer.Width, 
                                                            targetPixelBuffer.Height, 
                                                            row);
                });
                break;
            case ResizeMode.Bilinear:
                Parallel.For(0, targetPixelBuffer.Height, parameters.ParallelOptions, row =>
                {
                    ResizeImpl.ResizeBilinearSingle(sourceBuffer.GetChannel(0).AsSingle(), 
                                                            targetPixelBuffer.GetChannel(0).AsSingle(), 
                                                            sourceBuffer.Width, 
                                                            sourceBuffer.Height, 
                                                            targetPixelBuffer.Width, 
                                                            targetPixelBuffer.Height, 
                                                            row);
                    ResizeImpl.ResizeBilinearSingle(sourceBuffer.GetChannel(1).AsSingle(), 
                                                            targetPixelBuffer.GetChannel(1).AsSingle(), 
                                                            sourceBuffer.Width, 
                                                            sourceBuffer.Height, 
                                                            targetPixelBuffer.Width, 
                                                            targetPixelBuffer.Height, 
                                                            row);
                    ResizeImpl.ResizeBilinearSingle(sourceBuffer.GetChannel(2).AsSingle(), 
                                                            targetPixelBuffer.GetChannel(2).AsSingle(), 
                                                            sourceBuffer.Width, 
                                                            sourceBuffer.Height, 
                                                            targetPixelBuffer.Width, 
                                                            targetPixelBuffer.Height, 
                                                            row);
                });
                break;
            case ResizeMode.Bicubic:
                Parallel.For(0, targetPixelBuffer.Height, parameters.ParallelOptions, row =>
                {
                    ResizeImpl.ResizeBicubicSingle(sourceBuffer.GetChannel(0).AsSingle(), 
                                                            targetPixelBuffer.GetChannel(0).AsSingle(), 
                                                            sourceBuffer.Width, 
                                                            sourceBuffer.Height, 
                                                            targetPixelBuffer.Width, 
                                                            targetPixelBuffer.Height, 
                                                            row);
                    ResizeImpl.ResizeBicubicSingle(sourceBuffer.GetChannel(1).AsSingle(), 
                                                            targetPixelBuffer.GetChannel(1).AsSingle(), 
                                                            sourceBuffer.Width, 
                                                            sourceBuffer.Height, 
                                                            targetPixelBuffer.Width, 
                                                            targetPixelBuffer.Height, 
                                                            row);
                    ResizeImpl.ResizeBicubicSingle(sourceBuffer.GetChannel(2).AsSingle(), 
                                                            targetPixelBuffer.GetChannel(2).AsSingle(), 
                                                            sourceBuffer.Width, 
                                                            sourceBuffer.Height, 
                                                            targetPixelBuffer.Width, 
                                                            targetPixelBuffer.Height, 
                                                            row);
                });
                break;
        }

        return targetPixelBuffer;
    }

    private static PlanarPixelBuffer<Rgb888> ResizeRgb888(ResizerParameters parameters)
    {
        var targetPixelBuffer = new PlanarPixelBuffer<Rgb888>(parameters.Width, parameters.Height);
        var sourceBuffer = (ReadOnlyPlanarPixelBuffer<Rgb888>)parameters.Input!;
        switch (parameters.ResizeMode)
        {
            case ResizeMode.NearestNeighbor:
                Parallel.For(0, targetPixelBuffer.Height, parameters.ParallelOptions, row =>
                {
                    ResizeImpl.ResizeNearestNeighborByte(sourceBuffer.GetChannel(0).AsByte(), 
                                                            targetPixelBuffer.GetChannel(0).AsByte(), 
                                                            sourceBuffer.Width, 
                                                            sourceBuffer.Height, 
                                                            targetPixelBuffer.Width, 
                                                            targetPixelBuffer.Height, 
                                                            row);
                    ResizeImpl.ResizeNearestNeighborByte(sourceBuffer.GetChannel(1).AsByte(), 
                                                            targetPixelBuffer.GetChannel(1).AsByte(), 
                                                            sourceBuffer.Width, 
                                                            sourceBuffer.Height, 
                                                            targetPixelBuffer.Width, 
                                                            targetPixelBuffer.Height, 
                                                            row);
                    ResizeImpl.ResizeNearestNeighborByte(sourceBuffer.GetChannel(2).AsByte(), 
                                                            targetPixelBuffer.GetChannel(2).AsByte(), 
                                                            sourceBuffer.Width, 
                                                            sourceBuffer.Height, 
                                                            targetPixelBuffer.Width, 
                                                            targetPixelBuffer.Height, 
                                                            row);
                });
                break;
            case ResizeMode.Bilinear:
                Parallel.For(0, targetPixelBuffer.Height, parameters.ParallelOptions, row =>
                {
                    ResizeImpl.ResizeNearestNeighborByte(sourceBuffer.GetChannel(0).AsByte(), 
                                                            targetPixelBuffer.GetChannel(0).AsByte(), 
                                                            sourceBuffer.Width, 
                                                            sourceBuffer.Height, 
                                                            targetPixelBuffer.Width, 
                                                            targetPixelBuffer.Height, 
                                                            row);
                    ResizeImpl.ResizeNearestNeighborByte(sourceBuffer.GetChannel(1).AsByte(), 
                                                            targetPixelBuffer.GetChannel(1).AsByte(), 
                                                            sourceBuffer.Width, 
                                                            sourceBuffer.Height, 
                                                            targetPixelBuffer.Width, 
                                                            targetPixelBuffer.Height, 
                                                            row);
                    ResizeImpl.ResizeNearestNeighborByte(sourceBuffer.GetChannel(2).AsByte(), 
                                                            targetPixelBuffer.GetChannel(2).AsByte(), 
                                                            sourceBuffer.Width, 
                                                            sourceBuffer.Height, 
                                                            targetPixelBuffer.Width, 
                                                            targetPixelBuffer.Height, 
                                                            row);
                });
                break;
            case ResizeMode.Bicubic:
                Parallel.For(0, targetPixelBuffer.Height, parameters.ParallelOptions, row =>
                {
                    ResizeImpl.ResizeNearestNeighborByte(sourceBuffer.GetChannel(0).AsByte(), 
                                                            targetPixelBuffer.GetChannel(0).AsByte(), 
                                                            sourceBuffer.Width, 
                                                            sourceBuffer.Height, 
                                                            targetPixelBuffer.Width, 
                                                            targetPixelBuffer.Height, 
                                                            row);
                    ResizeImpl.ResizeNearestNeighborByte(sourceBuffer.GetChannel(1).AsByte(), 
                                                            targetPixelBuffer.GetChannel(1).AsByte(), 
                                                            sourceBuffer.Width, 
                                                            sourceBuffer.Height, 
                                                            targetPixelBuffer.Width, 
                                                            targetPixelBuffer.Height, 
                                                            row);
                    ResizeImpl.ResizeNearestNeighborByte(sourceBuffer.GetChannel(2).AsByte(), 
                                                            targetPixelBuffer.GetChannel(2).AsByte(), 
                                                            sourceBuffer.Width, 
                                                            sourceBuffer.Height, 
                                                            targetPixelBuffer.Width, 
                                                            targetPixelBuffer.Height, 
                                                            row);
                });
                break;
        }

        return targetPixelBuffer;
    }

    private static PlanarPixelBuffer<Rgb161616> ResizeRgb161616(ResizerParameters parameters)
    {
        var targetPixelBuffer = new PlanarPixelBuffer<Rgb161616>(parameters.Width, parameters.Height);
        var sourceBuffer = (ReadOnlyPlanarPixelBuffer<Rgb161616>)parameters.Input!;
        switch (parameters.ResizeMode)
        {
            case ResizeMode.NearestNeighbor:
                Parallel.For(0, targetPixelBuffer.Height, parameters.ParallelOptions, row =>
                {
                    ResizeImpl.ResizeNearestNeighborUInt16(sourceBuffer.GetChannel(0).AsUInt16(), 
                                                            targetPixelBuffer.GetChannel(0).AsUInt16(), 
                                                            sourceBuffer.Width, 
                                                            sourceBuffer.Height, 
                                                            targetPixelBuffer.Width, 
                                                            targetPixelBuffer.Height, 
                                                            row);
                    ResizeImpl.ResizeNearestNeighborUInt16(sourceBuffer.GetChannel(1).AsUInt16(), 
                                                            targetPixelBuffer.GetChannel(1).AsUInt16(), 
                                                            sourceBuffer.Width, 
                                                            sourceBuffer.Height, 
                                                            targetPixelBuffer.Width, 
                                                            targetPixelBuffer.Height, 
                                                            row);
                    ResizeImpl.ResizeNearestNeighborUInt16(sourceBuffer.GetChannel(2).AsUInt16(), 
                                                            targetPixelBuffer.GetChannel(2).AsUInt16(), 
                                                            sourceBuffer.Width, 
                                                            sourceBuffer.Height, 
                                                            targetPixelBuffer.Width, 
                                                            targetPixelBuffer.Height, 
                                                            row);
                });
                break;
            case ResizeMode.Bilinear:
                Parallel.For(0, targetPixelBuffer.Height, parameters.ParallelOptions, row =>
                {
                    ResizeImpl.ResizeNearestNeighborUInt16(sourceBuffer.GetChannel(0).AsUInt16(), 
                                                            targetPixelBuffer.GetChannel(0).AsUInt16(), 
                                                            sourceBuffer.Width, 
                                                            sourceBuffer.Height, 
                                                            targetPixelBuffer.Width, 
                                                            targetPixelBuffer.Height, 
                                                            row);
                    ResizeImpl.ResizeNearestNeighborUInt16(sourceBuffer.GetChannel(1).AsUInt16(), 
                                                            targetPixelBuffer.GetChannel(1).AsUInt16(), 
                                                            sourceBuffer.Width, 
                                                            sourceBuffer.Height, 
                                                            targetPixelBuffer.Width, 
                                                            targetPixelBuffer.Height, 
                                                            row);
                    ResizeImpl.ResizeNearestNeighborUInt16(sourceBuffer.GetChannel(2).AsUInt16(), 
                                                            targetPixelBuffer.GetChannel(2).AsUInt16(), 
                                                            sourceBuffer.Width, 
                                                            sourceBuffer.Height, 
                                                            targetPixelBuffer.Width, 
                                                            targetPixelBuffer.Height,
                                                            row);
                });
                break;
            case ResizeMode.Bicubic:
                Parallel.For(0, targetPixelBuffer.Height, parameters.ParallelOptions, row =>
                {
                    ResizeImpl.ResizeNearestNeighborUInt16(sourceBuffer.GetChannel(0).AsUInt16(), 
                                                            targetPixelBuffer.GetChannel(0).AsUInt16(), 
                                                            sourceBuffer.Width, 
                                                            sourceBuffer.Height, 
                                                            targetPixelBuffer.Width, 
                                                            targetPixelBuffer.Height, 
                                                            row);
                    ResizeImpl.ResizeNearestNeighborUInt16(sourceBuffer.GetChannel(1).AsUInt16(), 
                                                            targetPixelBuffer.GetChannel(1).AsUInt16(), 
                                                            sourceBuffer.Width, 
                                                            sourceBuffer.Height, 
                                                            targetPixelBuffer.Width, 
                                                            targetPixelBuffer.Height, 
                                                            row);
                    ResizeImpl.ResizeNearestNeighborUInt16(sourceBuffer.GetChannel(2).AsUInt16(), 
                                                            targetPixelBuffer.GetChannel(2).AsUInt16(), 
                                                            sourceBuffer.Width, 
                                                            sourceBuffer.Height, 
                                                            targetPixelBuffer.Width, 
                                                            targetPixelBuffer.Height, 
                                                            row);
                });
                break;
        }

        return targetPixelBuffer;
    }
}