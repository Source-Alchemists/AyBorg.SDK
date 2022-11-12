using AyBorg.SDK.ImageProcessing.Operations;

namespace AyBorg.SDK.ImageProcessing.Buffers.Operations;

public record ConvertParameters : IOperationParameters
{
    public ParallelOptions ParallelOptions { get; init; } = new ParallelOptions { MaxDegreeOfParallelism = 1 };
    public IReadOnlyPixelBuffer Input { get; init; } = null!;
    public Type OutputType { get; init; } = null!;
}