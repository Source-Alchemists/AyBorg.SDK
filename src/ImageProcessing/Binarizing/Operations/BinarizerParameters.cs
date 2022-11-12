using AyBorg.SDK.ImageProcessing.Buffers;
using AyBorg.SDK.ImageProcessing.Operations;

namespace AyBorg.SDK.ImageProcessing.Binarizing.Operations;

public record BinarizerParameters : IOperationParameters
{
    public ParallelOptions ParallelOptions { get; init; } = new ParallelOptions { MaxDegreeOfParallelism = System.Environment.ProcessorCount };
    
    public IReadOnlyPixelBuffer? Input { get; init; }
    
    public float Threshold { get; init; } = 0.5f;

    public BinaryThresholdMode Mode { get; init; } = BinaryThresholdMode.Lumincance;
}