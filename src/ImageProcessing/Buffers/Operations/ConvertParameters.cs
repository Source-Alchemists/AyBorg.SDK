using Atomy.SDK.ImageProcessing.Operations;

namespace Atomy.SDK.ImageProcessing.Buffers.Operations;

public record ConvertParameters : IOperationParameters
{
    public ParallelOptions ParallelOptions { get; init; } = new ParallelOptions { MaxDegreeOfParallelism = 1 };
    public IReadOnlyPixelBuffer Input { get; init; } = null!;
    public Type OutputType { get; init; } = null!;
}