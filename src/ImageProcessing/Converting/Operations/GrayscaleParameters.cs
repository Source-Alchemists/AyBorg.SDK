using AyBorg.SDK.ImageProcessing.Buffers;
using AyBorg.SDK.ImageProcessing.Operations;

namespace AyBorg.SDK.ImageProcessing.Converting.Operations;

public record GrayscaleConverterParameters : IOperationParameters
{
    public ParallelOptions ParallelOptions { get; init; } = new ParallelOptions { MaxDegreeOfParallelism = System.Environment.ProcessorCount };
    
    public IReadOnlyPixelBuffer Input { get; init; } = null!;

    public Type OutputType { get; init; } = null!;
}