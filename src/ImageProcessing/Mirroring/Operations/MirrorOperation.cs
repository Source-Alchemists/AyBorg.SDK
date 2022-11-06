using Autodroid.SDK.ImageProcessing.Buffers;
using Autodroid.SDK.ImageProcessing.Pixels;

namespace Autodroid.SDK.ImageProcessing.Mirroring.Operations;

public sealed class MirrorOperation : Mirror
{
    public MirrorOperation()
    {
        AddOperation<ReadOnlyPackedPixelBuffer<Mono>, PackedPixelBuffer<Mono>>(MirrorMono);
        AddOperation<ReadOnlyPackedPixelBuffer<Mono8>, PackedPixelBuffer<Mono8>>(MirrorMono8);
        AddOperation<ReadOnlyPackedPixelBuffer<Mono16>, PackedPixelBuffer<Mono16>>(MirrorMono16);

        AddOperation<ReadOnlyPackedPixelBuffer<Rgb>, PackedPixelBuffer<Rgb>>(MirrorRgb);
        AddOperation<ReadOnlyPackedPixelBuffer<Rgb24>, PackedPixelBuffer<Rgb24>>(MirrorRgb24);
        AddOperation<ReadOnlyPackedPixelBuffer<Rgb48>, PackedPixelBuffer<Rgb48>>(MirrorRgb48);

        AddOperation<ReadOnlyPlanarPixelBuffer<RgbFFF>, PlanarPixelBuffer<RgbFFF>>(MirrorRgbFFF);
        AddOperation<ReadOnlyPlanarPixelBuffer<Rgb888>, PlanarPixelBuffer<Rgb888>>(MirrorRgb888);
        AddOperation<ReadOnlyPlanarPixelBuffer<Rgb161616>, PlanarPixelBuffer<Rgb161616>>(MirrorRgb161616);
    }

    private static PackedPixelBuffer<Mono> MirrorMono(MirrorParameters parameters) => MirrorPacked((ReadOnlyPackedPixelBuffer<Mono>)parameters.Input!, parameters);
    private static PackedPixelBuffer<Mono8> MirrorMono8(MirrorParameters parameters) => MirrorPacked((ReadOnlyPackedPixelBuffer<Mono8>)parameters.Input!, parameters);
    private static PackedPixelBuffer<Mono16> MirrorMono16(MirrorParameters parameters) => MirrorPacked((ReadOnlyPackedPixelBuffer<Mono16>)parameters.Input!, parameters);
    private static PackedPixelBuffer<Rgb> MirrorRgb(MirrorParameters parameters) => MirrorPacked((ReadOnlyPackedPixelBuffer<Rgb>)parameters.Input!, parameters);
    private static PackedPixelBuffer<Rgb24> MirrorRgb24(MirrorParameters parameters) => MirrorPacked((ReadOnlyPackedPixelBuffer<Rgb24>)parameters.Input!, parameters);
    private static PackedPixelBuffer<Rgb48> MirrorRgb48(MirrorParameters parameters) => MirrorPacked((ReadOnlyPackedPixelBuffer<Rgb48>)parameters.Input!, parameters);
    private static PlanarPixelBuffer<RgbFFF> MirrorRgbFFF(MirrorParameters parameters) => MirrorPlanar((ReadOnlyPlanarPixelBuffer<RgbFFF>)parameters.Input!, parameters);
    private static PlanarPixelBuffer<Rgb888> MirrorRgb888(MirrorParameters parameters) => MirrorPlanar((ReadOnlyPlanarPixelBuffer<Rgb888>)parameters.Input!, parameters);
    private static PlanarPixelBuffer<Rgb161616> MirrorRgb161616(MirrorParameters parameters) => MirrorPlanar((ReadOnlyPlanarPixelBuffer<Rgb161616>)parameters.Input!, parameters);

    private static PackedPixelBuffer<TPixel> MirrorPacked<TPixel>(ReadOnlyPackedPixelBuffer<TPixel> sourcePixelBuffer, MirrorParameters parameters)
        where TPixel : unmanaged, IPackedPixel<TPixel>
    {
        var mode = parameters.MirrorMode;
        var targetPixelBuffer = new PackedPixelBuffer<TPixel>(sourcePixelBuffer.Width, sourcePixelBuffer.Height);

        switch (mode)
        {
            case MirrorMode.Horizontal:
                Parallel.For(0, sourcePixelBuffer.Height, parameters.ParallelOptions, rowIndex =>
                {
                    var sourceRow = sourcePixelBuffer.GetRow(rowIndex);
                    var targetRow = targetPixelBuffer.GetRow(rowIndex);
                    int z = targetRow.Length - 1;
                    for (int x = 0; x < sourceRow.Length; x++)
                    {
                        targetRow[z--] = sourceRow[x];
                    }
                });
                break;
            case MirrorMode.Vertical:
                int lastColumnIndex = sourcePixelBuffer.Height - 1;
                Parallel.For(0, sourcePixelBuffer.Height, parameters.ParallelOptions, rowIndex =>
                {
                    sourcePixelBuffer.GetRow(rowIndex).CopyTo(targetPixelBuffer.GetRow(lastColumnIndex - rowIndex));
                });
                break;
            case MirrorMode.VerticalHorizontal:
                Parallel.For(0, sourcePixelBuffer.Height, parameters.ParallelOptions, rowIndex =>
                {
                    int stride = rowIndex * sourcePixelBuffer.Width;
                    int tPos = (targetPixelBuffer.Height - rowIndex) * targetPixelBuffer.Width - 1;
                    for (int x = 0; x < targetPixelBuffer.Width; x++)
                    {
                        targetPixelBuffer.Pixels[tPos - x] = sourcePixelBuffer.Pixels[stride + x];
                    }
                });
                break;
        }

        return targetPixelBuffer;
    }

    private static PlanarPixelBuffer<TPixel> MirrorPlanar<TPixel>(ReadOnlyPlanarPixelBuffer<TPixel> sourcePixelBuffer, MirrorParameters parameters)
        where TPixel : unmanaged, IPlanarPixel<TPixel>
    {
        var mode = parameters.MirrorMode;
        var targetPixelBuffer = new PlanarPixelBuffer<TPixel>(sourcePixelBuffer.Width, sourcePixelBuffer.Height);

        switch (mode)
        {
            case MirrorMode.Horizontal:
                for (int channelIndex = 0; channelIndex < sourcePixelBuffer.NumberOfChannels; channelIndex++)
                {
                    Parallel.For(0, sourcePixelBuffer.Height, parameters.ParallelOptions, rowIndex =>
                    {
                        var sourceRow = sourcePixelBuffer.GetRow(channelIndex, rowIndex);
                        var targetRow = targetPixelBuffer.GetRow(channelIndex, rowIndex);
                        int z = targetRow.Length - 1;
                        for (int x = 0; x < sourceRow.Length; x++)
                        {
                            targetRow[z--] = sourceRow[x];
                        }
                    });
                }
                break;
            case MirrorMode.Vertical:
                for (int channelIndex = 0; channelIndex < sourcePixelBuffer.NumberOfChannels; channelIndex++)
                {
                    int lastColumnIndex = sourcePixelBuffer.Height - 1;
                    Parallel.For(0, sourcePixelBuffer.Height, rowIndex =>
                    {
                        sourcePixelBuffer.GetRow(channelIndex, rowIndex).CopyTo(targetPixelBuffer.GetRow(channelIndex, lastColumnIndex - rowIndex));
                    });
                }
                break;
            case MirrorMode.VerticalHorizontal:
                for (int channelIndex = 0; channelIndex < sourcePixelBuffer.NumberOfChannels; channelIndex++)
                {
                    Parallel.For(0, sourcePixelBuffer.Height, rowIndex =>
                    {
                        int stride = rowIndex * sourcePixelBuffer.Width;
                        int tPos = (targetPixelBuffer.Height - rowIndex) * targetPixelBuffer.Width - 1;
                        for (int x = 0; x < targetPixelBuffer.Width; x++)
                        {
                            targetPixelBuffer.GetChannel(channelIndex)[tPos - x] = sourcePixelBuffer.GetChannel(channelIndex)[stride + x];
                        }
                    });
                }
                break;
        }
        return targetPixelBuffer;
    }
}