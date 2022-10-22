using Atomy.SDK.ImageProcessing.Buffers;
using Atomy.SDK.Operations;

namespace Atomy.SDK.ImageProcessing.Converting.Operations;

public record GrayscaleConverterParameters : IOperationParameters
{
    public ParallelOptions ParallelOptions { get; init; } = new ParallelOptions { MaxDegreeOfParallelism = System.Environment.ProcessorCount };
    
    public IReadOnlyPixelBuffer Input { get; init; } = null!;

    public Type OutputType { get; init; } = null!;
}