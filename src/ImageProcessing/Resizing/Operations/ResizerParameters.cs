using AyBorg.SDK.ImageProcessing.Buffers;
using AyBorg.SDK.ImageProcessing.Operations;

namespace AyBorg.SDK.ImageProcessing.Resizing.Operations;

public record ResizerParameters : IOperationParameters
{
    public ParallelOptions ParallelOptions { get; init; } = new ParallelOptions { MaxDegreeOfParallelism = System.Environment.ProcessorCount };
    
    public IReadOnlyPixelBuffer? Input { get; init; }
    
    public int Width { get; init; } = 100;
    
    public int Height { get; init; } = 100;
    
    public ResizeMode ResizeMode { get; init; } = ResizeMode.Bilinear;
}